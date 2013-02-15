using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryMineralAndFossilType
    {
        public DictionaryMineralAndFossilType(Session session) : base(session) { }
        public DictionaryMineralAndFossilType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
