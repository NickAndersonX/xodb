using System;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using Ionic.Zip;

namespace XODB.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public static int CurrentVersion { get { return 3; } }
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            object o = null;
            if (IsTableExists("X_PrivateData"))
                o = ExecuteScalarCommand("select [value] from X_PrivateData where UniqueID='XODBSchemaVersion'", false);
            int xodbSchemaVersion = (o == null) ? -1 : Convert.ToInt32(o);
            if (xodbSchemaVersion == -1)
            {
                ExecuteNonQueryCommand(Properties.Resources.XODBSchema1, false);
                ExecuteNonQueryCommand(Properties.Resources.XODBSchema1Data, false);
            }
            else if (xodbSchemaVersion != CurrentVersion && System.Windows.Forms.Application.ProductName.Contains("Win") && System.Windows.Forms.Application.ProductName.Contains("XODB"))
            {
                //Do backup
                try
                {
                    var f = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "xodb.bak");
                    foreach (var s in string.Format(Properties.Resources.XODBBackup, f).Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)) ExecuteNonQueryCommand(s, false);
                }
                catch { }
            }
            if (xodbSchemaVersion < 2)
                foreach (var s in Properties.Resources.XODBSchema2.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)) ExecuteNonQueryCommand(s, true);
            if (xodbSchemaVersion < 3)
                foreach (var s in Properties.Resources.XODBSchema3.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)) ExecuteNonQueryCommand(s, true); //This may have errors
            //Reboot
            try
            {
                if (xodbSchemaVersion != CurrentVersion && System.Windows.Forms.Application.ProductName.Contains("Win") && System.Windows.Forms.Application.ProductName.Contains("XODB"))
                {
                    System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            catch { }
            base.UpdateDatabaseBeforeUpdateSchema();
        }

        protected override System.Data.IDbCommand CreateCommand(string commandText)
        {
            return base.CreateCommand(commandText);
        }

    }
}
