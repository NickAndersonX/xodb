using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;

namespace XODB.Import.ImportUtils
{
    public class ProjectUtils
    {



        internal static Dictionary<Guid, string> GetProjectList()
        {
            Dictionary<Guid, string> projectList = new Dictionary<Guid,string>();
            XODBImportEntities resourceModels = new XODBImportEntities();
            var result = from p in resourceModels.X_Project select new { p.ProjectName, p.ProjectID };
            foreach (var res in result) { 
                projectList.Add(res.ProjectID, res.ProjectName);                
                
            }
            return projectList;

        }
    }
}
