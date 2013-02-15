using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryUsageAuditType
    {
        public DictionaryUsageAuditType(Session session) : base(session) { }
        public DictionaryUsageAuditType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
