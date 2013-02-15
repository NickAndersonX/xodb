using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;
using XODB.ViewModels;

namespace XODB.Services
{
    [ServiceContract]
    public interface IParametersService : IDependency {

        [OperationContract]
        IEnumerable<Unit> GetUnits();

        [OperationContract]
        BlockModelParameterViewModel GetParameter(Guid id);

        [OperationContract]
        BlockModelApproveViewModel GetApproval(Guid id);
    }
}