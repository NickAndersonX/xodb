using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProjectPlanTaskModel
    {
        public ProjectPlanTaskModel(Session session) : base(session) { }
        public ProjectPlanTaskModel() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
