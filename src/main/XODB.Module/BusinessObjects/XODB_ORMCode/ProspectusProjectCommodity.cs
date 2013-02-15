using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProspectusProjectCommodity
    {
        public ProspectusProjectCommodity(Session session) : base(session) { }
        public ProspectusProjectCommodity() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
