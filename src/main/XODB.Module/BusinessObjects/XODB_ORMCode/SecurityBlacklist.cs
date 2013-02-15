using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class SecurityBlacklist
    {
        public SecurityBlacklist(Session session) : base(session) { }
        public SecurityBlacklist() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
