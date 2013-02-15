using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProjectPlanTask
    {
        public ProjectPlanTask(Session session) : base(session) { }
        public ProjectPlanTask() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
