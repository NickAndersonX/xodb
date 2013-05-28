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

    public partial class ShowDocumentsController : ViewController {

        public ShowDocumentsController() {
            PopupWindowShowAction showDocumentsAction = new PopupWindowShowAction(this, "Show Documents", "View");
            showDocumentsAction.ImageName = "BO_Folder";
            showDocumentsAction.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(showDocumentsAction_CustomizePopupWindowParams);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(object);
        }


        void showDocumentsAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e) {

            try
            {
                XPLiteObject o = e.Action.SelectionContext.CurrentObject as XPLiteObject;
                var value = o.This.GetType().GetProperty(o.ClassInfo.KeyProperty.Name).GetValue(o.This) as Guid?;
                var table = o.ClassInfo.TableName;

                IObjectSpace objectSpace = Application.CreateObjectSpace();
                CollectionSource collectionSource = new CollectionSource(objectSpace, typeof(FileData));
                CriteriaOperator c1 = new BinaryOperator(
                    new OperandProperty("TableType"), table,
                    BinaryOperatorType.Equal
                );
                CriteriaOperator c2 = new BinaryOperator(
                    new OperandProperty("ReferenceID"), value,
                    BinaryOperatorType.Equal
                );
                CriteriaOperator co = c1 & c2;
                collectionSource.Criteria.Add("Refences", co);
                //Do this for manual data lists
                //if ((collectionSource.Collection as XPBaseCollection) != null)
                //{
                //    ((XPBaseCollection)collectionSource.Collection).LoadingEnabled = false;
                //}
                ListView view = Application.CreateListView(Application.GetListViewId(typeof(FileData)), collectionSource, false);
                view.Editor.AllowEdit = true;
                view.AllowNew.Clear();                
                e.View = view;
                e.DialogController.SaveOnAccept = false;
            }
            catch (Exception ex)
            { }


        }
    }
   
}