using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;

namespace XODB.Services
{
     [ServiceContract]
    public interface IUsersService : IDependency 
    {
         [OperationContract]
         void SyncUsers();

         [OperationContract]
         IEnumerable<Contact> GetContacts();

         [OperationContract]
         Guid GetUserID(string username);

         [OperationContract]
         string[] GetUserEmails(Guid[] users);

         [OperationContract]
         void EmailUsers(string[] recipients, string subject, string body, bool retry=false);

         [OperationContract]
         void EmailUsersAsync(string[] recipients, string subject, string body);
    }
}