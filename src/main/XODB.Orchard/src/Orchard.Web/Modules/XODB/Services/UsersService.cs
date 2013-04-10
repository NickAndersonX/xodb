using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Ionic.Zip;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.FileSystems.Media;
using Orchard.Localization;
using XODB.Models;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Validation;
using Orchard;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using Orchard.Roles.Services;
using Orchard.Users.Services;
using Orchard.Users.Models;
using System.Text.RegularExpressions;
using System.Transactions;
using Orchard.Messaging.Services;
using Orchard.Logging;
using XODB.Helpers;
using Orchard.Tasks.Scheduling;
using Orchard.Data;

namespace XODB.Services {
    
    [UsedImplicitly]
    public class UsersService : IUsersService {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IMessageManager _messageManager;
        private readonly IScheduledTaskManager _taskManager;
        private readonly PrincipalContext _securityContext = new PrincipalContext(ContextType.Domain); //TODO: May look at others one day
        private readonly IRepository<EmailPartRecord> _repository;
        public ILogger Logger { get; set; }


        public UsersService(IContentManager contentManager, IOrchardServices orchardServices, IRoleService roleService, IUserService userService, IMessageManager messageManager, IScheduledTaskManager taskManager, IRepository<EmailPartRecord> repository)
        {
            _repository = repository;
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _roleService = roleService;
            _userService = userService;
            _messageManager = messageManager;
            _taskManager = taskManager;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }

        public void SyncUsers()
        {

            //Get Orchard Users
            var orchardusers = _contentManager.Query<UserPart, UserPartRecord>().List();

            //Get AD Users
            // throw new NotImplementedException();
            // get a DirectorySearcher object
            DirectorySearcher search = new DirectorySearcher();

            // specify the search filter
            search.Filter = "(&(objectCategory=person)(objectClass=user))";
            //search.Filter = "(&(objectClass=user)(anr=agrosser))"; //TEST
            
            //// specify which property values to return in the search
            search.PropertiesToLoad.Add("name");   // first name
            search.PropertiesToLoad.Add("givenName");   // first name
            search.PropertiesToLoad.Add("sn");          // last name
            search.PropertiesToLoad.Add("mail");        // smtp mail address
            search.PropertiesToLoad.Add("samaccountname");        // account name
            search.PropertiesToLoad.Add("memberof");        // groups
            search.PropertiesToLoad.Add("objectsid");
            search.PropertiesToLoad.Add("objectguid");
            search.PropertiesToLoad.Add("title");

            // perform the search
            SearchResultCollection results = search.FindAll(); //.FindOne();

            var sessionRoleCache = new Dictionary<string,string>();
            var adusers = from SearchResult o in results
                       select new
                           {
                               name = o.Properties["name"] != null && o.Properties["name"].Count > 0 ? string.Format("{0}", o.Properties["name"][0]) : null,
                               givenName = o.Properties["givenName"] != null && o.Properties["givenName"].Count > 0 ? string.Format("{0}", o.Properties["givenName"][0]) : null,
                               sn = o.Properties["sn"] != null && o.Properties["sn"].Count > 0 ? string.Format("{0}", o.Properties["sn"][0]) : null,
                               email = o.Properties["mail"] != null && o.Properties["mail"].Count > 0 ? string.Format("{0}", o.Properties["mail"][0]) : null,
                               samaccountname = o.Properties["samaccountname"] != null && o.Properties["samaccountname"].Count > 0 ? string.Format("{0}", o.Properties["samaccountname"][0]) : null,
                               username = o.Properties["objectsid"] != null && o.Properties["objectsid"].Count > 0 ? ((NTAccount)(new SecurityIdentifier((byte[])o.Properties["objectsid"][0], 0)).Translate(typeof(NTAccount))).ToString() : null,
                               guid = o.Properties["objectguid"] != null && o.Properties["objectguid"].Count > 0 ? new Guid((byte[])o.Properties["objectguid"][0]) : (Guid?)null,
                               title = o.Properties["title"] != null && o.Properties["title"].Count > 0 ? string.Format("{0}", o.Properties["title"][0]) : null,
                               roles = o.Properties["memberof"] != null ? (from string m in o.Properties["memberof"] select getNameFromFQDN(m, sessionRoleCache)).ToArray() : new string[] { }
                           };

            //Get XODB Users
            Contact[] xodbusers;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ContactsDataContext();
                xodbusers = (from o in d.Contacts select o).ToArray();

                //Sync AD, Orchard, XODB
                //New into XODB
                //We need firstname, surname
                var ad_new = (from o in adusers where o.givenName != null && o.sn != null && (o.guid.HasValue && !(from x in xodbusers select x.ContactID).Contains((Guid)o.guid)) || (!o.guid.HasValue && !(from x in xodbusers select x.Username.ToLowerInvariant()).Contains(o.username.ToLowerInvariant())) select o);
                
                foreach (var o in ad_new)
                {
                    Contact c = new Contact();
                    c.ContactID = o.guid.HasValue ? o.guid.Value : Guid.NewGuid();
                    c.Username = o.username;
                    c.Firstname = o.givenName;
                    c.ContactName = o.name;
                    c.Surname = o.sn;
                    c.DefaultEmail = o.email;
                    d.Contacts.InsertOnSubmit(c);
                }

                //Updates into XODB
                var ad_diff = from o in adusers
                              from x in xodbusers
                              where ((o.guid.HasValue && o.guid.Value == x.ContactID) || (o.username != null && x.Username!=null && o.username.ToLowerInvariant() == x.Username.ToLowerInvariant()))
                                      //Things to update
                                      && ( 
                                      o.givenName != x.Firstname
                                      || o.sn != x.Surname
                                      || o.email != x.DefaultEmail
                                      || o.name != x.ContactName
                              ) select new {x.ContactID, o.givenName, o.sn, o.email, o.name};
                foreach (var o in ad_diff)
                {
                    var c = xodbusers.First(x => x.ContactID == o.ContactID);
                    c.Firstname = o.givenName;
                    c.ContactName = o.name;
                    c.Surname = o.sn;
                    c.DefaultEmail = o.email;
                }
                d.SubmitChanges();                          

                //New into Orchard
                //TODO: Could do it? No. Let's let the AD Module handle this for now.

                //Let's ignore AD for now too...
                
            }


        }

        public IEnumerable<Contact> GetContacts()
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dataContext = new ContactsDataContext();
                return dataContext.Contacts.OrderBy(x=>x.ContactName).ToArray();
            }
        }

        public string[] GetUserEmails(Guid[] users)
        {
            if (users == null || users.Length == 0)
                return new string[] { };
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ContactsDataContext();
                var o = from c in d.Contacts where users.Contains(c.ContactID) && c.DefaultEmail != null
                        select c.DefaultEmail;
                return o.ToArray();
            }
        }

        public Guid GetUserID(string username)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ContactsDataContext();
                return d.Contacts.Where(x=>x.Username == username).Select(x=>x.ContactID).FirstOrDefault();
            }
        }

        private string getNameFromFQDN(string fqdn, Dictionary<string,string> cache)
        {
            string temp;
            if (cache != null && cache.TryGetValue(fqdn, out temp))
                return temp;
            try
            {
                temp = ((NTAccount)(GroupPrincipal.FindByIdentity(_securityContext, string.Format("{0}", fqdn)).Sid).Translate(typeof(NTAccount))).ToString();
            }
            catch
            {
                temp = null;
            }
            cache.Add(fqdn, temp);
            return temp;
        }

        public void EmailUsers(string[] emails, string subject, string body, bool retry = false)
        {
            var data = new Dictionary<string,string>();
            data.Add("Subject", subject); 
            data.Add("Body", body);
            Orchard.Email.Models.SmtpSettingsPart smtpSettings = null;
            if (_orchardServices.WorkContext != null) smtpSettings = _orchardServices.WorkContext.CurrentSite.As<Orchard.Email.Models.SmtpSettingsPart>();
            try
            {
                if (emails == null)
                    emails = new string[] { };
                var recipients = emails.Union(new string[] { smtpSettings.Address }).Where(f => !string.IsNullOrEmpty(f)).ToArray();
                _messageManager.Send(recipients, XODB.Events.EmailMessageHandler.DEFAULT_XODB_EMAIL_TYPE, "email", data);
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    EmailUsersAsync(emails, subject, body);
                }
            }
            finally
            {
                Logger.Information(string.Format("Attempted sending notification for: {0}.\r\n\r\n Regarding: \r\n\r\n {1}\n\nRetry:{2}", subject, body, retry));
            }
        }

        public void EmailUsersAsync(string[] emails, string subject, string body)
        {
            try
            {
                var em = _contentManager.New<EmailPart>("Email");
                em.Recipients = emails.FlattenStringArray();
                em.Subject = subject;
                em.Body = body;
                em.Retry = false;
                em.Processed = DateTime.UtcNow;
                _contentManager.Create(em, VersionOptions.Published);
                _taskManager.EmailAsync(em.ContentItem);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex, string.Format("Failed Sending Notification - Couldn't assemble message.\n\nSubject:\n{0}\n\nBody:\n{1}\n\nRecipients:\n{2}\n\n", subject, body, emails == null ? "Unknown Recipients" : string.Join(";", emails)));
            }
        }

    }
}
