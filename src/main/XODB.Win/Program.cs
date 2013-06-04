using System;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using XODB.Module;

namespace XODB.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if EASYTEST
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            XODBWindowsFormsApplication winApplication = new XODBWindowsFormsApplication();
#if EASYTEST
			if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
			}
#endif
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            try
            {
                winApplication.Setup();
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed && System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun && System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion != System.Deployment.Application.ApplicationDeployment.CurrentDeployment.UpdatedVersion)
                {
                    using (var f = new XODB.Module.Win.Controllers.UpdateConfig())
                    {
                        f.ShowDialog();
                    }
                }
                if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null && AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null && AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length > 0)
                {
                    //Kill other processes
                    var p = Process.GetCurrentProcess();
                    Process[] ps = Process.GetProcessesByName(p.ProcessName);
                    foreach (var e in ps)
                    {
                        if (e.Id != p.Id)
                            e.Kill();
                    }
                    //Now update file and start
                    using (var f = File.Open(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0], FileMode.Open, FileAccess.Read))
                    {
                        using (var z = f.ReadConfigFromPackage())
                        {
                            var path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
                            path.WriteUserConfigFile(z);
                            z.Close();
                        }
                        f.Close();
                    }
                }

                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }
    }
}
