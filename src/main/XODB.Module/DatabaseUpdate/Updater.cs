using System;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;

namespace XODB.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            object o = ExecuteScalarCommand("select [value] from X_PrivateData where UniqueID='XODBSchemaVersion'", false);
            int xodbSchemaVersion = (o == null) ?  -1 : Convert.ToInt32(o);
            if (xodbSchemaVersion == -1)
                ExecuteNonQueryCommand(Properties.Resources.XODBSchema1Data, false);
            if (xodbSchemaVersion < 2)
                foreach (var s in Properties.Resources.XODBSchema2.Split(new string[] {"GO"}, StringSplitOptions.RemoveEmptyEntries)) ExecuteNonQueryCommand(s, false);
            if (xodbSchemaVersion < 3)
                foreach (var s in Properties.Resources.XODBSchema3.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)) ExecuteNonQueryCommand(s, false);
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            object o = null;
            if (IsTableExists("X_PrivateData"))
                o = ExecuteScalarCommand("select [value] from X_PrivateData where UniqueID='XODBSchemaVersion'", false);
            int xodbSchemaVersion = (o == null) ? -1 : Convert.ToInt32(o);
            if (xodbSchemaVersion == -1)
                ExecuteNonQueryCommand(Properties.Resources.XODBSchema1, false);
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
