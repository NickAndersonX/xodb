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

    public class SendConfigController : NewObjectViewController
    {

        public SendConfigController()
        {
            var myAction = new SimpleAction(this, "Share Configuration", DevExpress.Persistent.Base.PredefinedCategory.Tools);
            myAction.Execute += myAction_Execute;
            myAction.ImageName = "BO_Contact";
            Actions.Add(myAction);

        }

        void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var f = new XODB.Module.Win.Controllers.SendConfig();
            f.ShowDialog();
        }
    }
}
