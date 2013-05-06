using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProjectPlanTaskComplianceResponse
    {
        public ProjectPlanTaskComplianceResponse(Session session) : base(session) { }
        public ProjectPlanTaskComplianceResponse() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
