using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class BlockModelBlockData
    {
        public BlockModelBlockData(Session session) : base(session) { }
        public BlockModelBlockData() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
