using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class WarehouseModelSupplier
    {
        public WarehouseModelSupplier(Session session) : base(session) { }
        public WarehouseModelSupplier() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
