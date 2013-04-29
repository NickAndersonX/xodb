using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;
using Orchard.Media.Models;
using XODB.ViewModels;
using System.Threading.Tasks;
using Orchard.ContentManagement;

namespace XODB.Services
{
     [ServiceContract]
    public interface IAssayService : IDependency 
    {        

         [OperationContract]
         Task<IReport> ReportAssaysAsync(AssayReportViewModel m);

         [OperationContract]
         IReport ReportAssays(AssayReportViewModel m);

    }
}