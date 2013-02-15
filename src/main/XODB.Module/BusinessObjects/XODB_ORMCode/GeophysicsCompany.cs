using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class GeophysicsCompany
    {
        public GeophysicsCompany(Session session) : base(session) { }
        public GeophysicsCompany() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
