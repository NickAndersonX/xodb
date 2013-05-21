using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DicitonaryAssetStatus
    {
        public DicitonaryAssetStatus(Session session) : base(session) { }
        public DicitonaryAssetStatus() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
