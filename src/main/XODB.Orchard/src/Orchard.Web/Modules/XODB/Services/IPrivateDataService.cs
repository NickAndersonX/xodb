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
    public interface IPrivateDataService : IDependency {

        [OperationContract]
        T GetObject<T>(string lookup);

        [OperationContract]
        Guid? GetKey(string lookup);

        [OperationContract]
        MetaData[] GetMetadata(Guid referenceID, Func<MetaData, bool> predicate = null);

        [OperationContract]
        T GetFirstMetadata<T>(Guid referenceID, Func<MetaData, bool> predicate = null);

        Guid XODB_GUID_LOG
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }

        Guid XODB_BM_STAGE
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }
    }
}