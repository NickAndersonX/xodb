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

    public partial class NewDocumentController : NewObjectViewController {

        public NewDocumentController()        
        {
            var myAction = new SimpleAction(this, "Add Document", DevExpress.Persistent.Base.PredefinedCategory.View);
            myAction.TargetObjectType = typeof(object);
            myAction.Execute += myAction_Execute;
            myAction.ImageName = "BO_FileAttachment";
            Actions.Add(myAction);
            this.FrameAssigned += NewDocumentController_FrameAssigned;
            ObjectCreated += NewDocumentController_ObjectCreated;
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(object);
        }

        void NewDocumentController_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            if (e.CreatedObject is FileData)
            {
                try
                {

                    var view = sender as DevExpress.ExpressApp.ViewController;
                    if (view == null || view.View == null || view.View.SelectedObjects == null || view.View.SelectedObjects.Count < 1)
                        return;
                    XPLiteObject o = view.View.SelectedObjects[0] as XPLiteObject;
                    ((FileData)e.CreatedObject).ReferenceID = (Guid)o.This.GetType().GetProperty(o.ClassInfo.KeyProperty.Name).GetValue(o.This);
                    ((FileData)e.CreatedObject).TableType = o.ClassInfo.TableName;
                }
                catch { }
            }
        }

        void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var args = new SingleChoiceActionExecuteEventArgs(e.Action, e.Action.SelectionContext, new ChoiceActionItem("NewObject", typeof(FileData)));
            New(args);
            e.ShowViewParameters.Assign(args.ShowViewParameters);
        }

        void NewDocumentController_FrameAssigned(object sender, System.EventArgs e)
        {
            NewObjectViewController standardController = Frame.GetController<NewObjectViewController>();
            standardController.ObjectCreated += NewDocumentController_ObjectCreated;
        }

       
    }
   
}