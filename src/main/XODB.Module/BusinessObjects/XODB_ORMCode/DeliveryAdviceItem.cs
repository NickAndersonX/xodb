using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DeliveryAdviceItem
    {
        public DeliveryAdviceItem(Session session) : base(session) { }
        public DeliveryAdviceItem() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
