using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DrillCompanyContract
    {
        public DrillCompanyContract(Session session) : base(session) { }
        public DrillCompanyContract() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
