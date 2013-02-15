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
using System.Transactions;

namespace XODB.Services {

    [UsedImplicitly]
    public class ProjectsService : IProjectsService {
        private readonly IOrchardServices _orchardServices;
        private readonly IUsersService _userServices;
        public ProjectsService(IOrchardServices orchardServices, IUsersService userService)
        {
            _userServices = userService;
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

   
        public IEnumerable<Project> GetProjects() {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dataContext = new ProjectsDataContext();
                return dataContext.Projects.ToArray();
            }
        }

        public IEnumerable<ProjectPlanTask> GetStages(Guid ProjectID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dataContext = new ProjectsDataContext();
                return dataContext.ProjectPlanTasks.Where(x=>x.ProjectPlan.ProjectID == ProjectID).ToArray();
            }
        }

        public void DeleteProject(Guid projectID, Guid contactID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ProjectsDataContext();
                var o = d.Projects.Where(x => x.ProjectID == projectID).Single();
                o.VersionDeletedBy  = contactID;
                o.VersionUpdated = DateTime.Now;
                d.SubmitChanges();
            }
        }

        public void EmailAllProjectOwners(string subject, string body)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ProjectsDataContext();
                var o = d.Projects.Where(x => x.VersionOwnerContactID != null).Select(f=>(Guid)f.VersionOwnerContactID).ToArray();
                _userServices.EmailUsers(o, subject, body);
            }
        }

    }
}
