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
         void EmailUsers(Guid[] recipients, string subject, string body);
    }
}