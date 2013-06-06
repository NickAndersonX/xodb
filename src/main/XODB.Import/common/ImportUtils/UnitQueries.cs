using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import.DataModels;

namespace XODB.Import.ImportUtils
{
    public class UnitQueries
    {


        internal X_DictionaryUnit FindUnits(string theUnit)
        {
            XODBImportEntities xm = new XODBImportEntities();
            
            X_DictionaryUnit xd = (from c in xm.X_DictionaryUnit where c.StandardUnitName.Trim().Equals(theUnit) select c).FirstOrDefault();
            return xd;

        }
    }
}
