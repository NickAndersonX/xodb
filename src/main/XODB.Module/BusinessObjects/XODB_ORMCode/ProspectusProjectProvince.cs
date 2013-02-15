using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ProspectusProjectProvince
    {
        public ProspectusProjectProvince(Session session) : base(session) { }
        public ProspectusProjectProvince() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
