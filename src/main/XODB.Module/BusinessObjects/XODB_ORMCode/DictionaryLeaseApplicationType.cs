using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryLeaseApplicationType
    {
        public DictionaryLeaseApplicationType(Session session) : base(session) { }
        public DictionaryLeaseApplicationType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
