using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProjectPlanTaskWorker
    {
        public ProjectPlanTaskWorker(Session session) : base(session) { }
        public ProjectPlanTaskWorker() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
