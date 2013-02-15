using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class BlockModelMetadata
    {
        public BlockModelMetadata(Session session) : base(session) { }
        public BlockModelMetadata() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
