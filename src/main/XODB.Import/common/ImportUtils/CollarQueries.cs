using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;
using XODB.Import.DataWrappers;

namespace XODB.Import.ImportUtils
{
    public class CollarQueries
    {


        internal static List<CollarInfo> FindCollarsForProject(Guid currentSelectedProject)
        {
            List<CollarInfo> ss = new List<CollarInfo>();
            XODBImportCollarEntities e = new XODBImportCollarEntities();
            string cs = e.Database.Connection.ConnectionString;
            IQueryable<X_Header> res = e.X_Header.Where(c => c.ProjectID == currentSelectedProject);
            foreach (X_Header xx in res)
            {
                CollarInfo ci = new CollarInfo();
                ci.Name = xx.HoleName;
                ci.Easting = (double)xx.Easting;
                ci.Northing = (double)xx.Northing;
                ci.RL = (double)xx.Elevation;
                ss.Add(ci);
            }

            return ss;
        }

        internal static Guid FindHeaderGuid(string headerNameItem, Guid currentSelectedProject)
        {
            Guid resHole = new Guid();
            XODBImportCollarEntities e = new XODBImportCollarEntities();

            IQueryable<X_Header> res = e.X_Header.Where(c => (c.ProjectID == currentSelectedProject) && (c.HoleName.Equals(headerNameItem)) );
            foreach (X_Header xx in res)
            {
                resHole = xx.HeaderID;
            }

            return resHole;
        }

        internal static Dictionary<string, Guid> FindHeaderGuidsForProject(Guid XODBProjectID)
        {
            Dictionary<string, Guid> holeIDLookups = new Dictionary<string, Guid>();
            
            XODBImportCollarEntities e = new XODBImportCollarEntities();

            IQueryable<X_Header> res = e.X_Header.Where(c => (c.ProjectID == XODBProjectID) );
            foreach (X_Header xx in res)
            {
                Guid resHole = xx.HeaderID;
                string ss = xx.HoleName;
                // only add if it does not exist
                bool exists = holeIDLookups.ContainsKey(ss);
                if (!exists)
                {
                    holeIDLookups.Add(ss, resHole);
                }
            }

            return holeIDLookups;
        }
    }
}
