using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;
using Orchard.ContentManagement;

namespace XODB.Services
{
    [ServiceContract]
    public interface IConcurrentTaskService : IDependency {

        [OperationContract]
        void ExecuteAsyncTask(Action<ContentItem> task, ContentItem data);
    }

}