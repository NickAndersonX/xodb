using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDeliveryAdviceReason
    {
        public DictionaryDeliveryAdviceReason(Session session) : base(session) { }
        public DictionaryDeliveryAdviceReason() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
