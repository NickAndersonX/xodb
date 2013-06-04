using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using XODB.Module.BusinessObjects.XODB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.SystemModule;
//using DevExpress.Persistent.BaseImpl;

namespace XODB.Module.Win.Controllers
{

    public class UpdateConfigController : NewObjectViewController
    {

        public UpdateConfigController()
        {
            var myAction = new SimpleAction(this, "Update Configuration", DevExpress.Persistent.Base.PredefinedCategory.Tools);
            myAction.Execute += myAction_Execute;
            myAction.ImageName = "ModelEditor_Action_Modules";
            Actions.Add(myAction);

        }

        void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var f = new XODB.Module.Win.Controllers.UpdateConfig();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.Windows.Forms.Application.Restart();
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
