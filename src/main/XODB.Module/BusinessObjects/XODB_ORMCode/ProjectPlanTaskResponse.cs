using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProjectPlanTaskResponse
    {
        public ProjectPlanTaskResponse(Session session) : base(session) { }
        public ProjectPlanTaskResponse() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
