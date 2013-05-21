using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ModelProcedureTaskPart
    {
        public ModelProcedureTaskPart(Session session) : base(session) { }
        public ModelProcedureTaskPart() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
