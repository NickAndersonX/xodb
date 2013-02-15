using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class SecurityWhitelist
    {
        public SecurityWhitelist(Session session) : base(session) { }
        public SecurityWhitelist() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
