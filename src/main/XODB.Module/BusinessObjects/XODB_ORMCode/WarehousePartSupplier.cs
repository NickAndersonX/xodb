using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class WarehousePartSupplier
    {
        public WarehousePartSupplier(Session session) : base(session) { }
        public WarehousePartSupplier() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
