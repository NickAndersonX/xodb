using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryMineralAbundance
    {
        public DictionaryMineralAbundance(Session session) : base(session) { }
        public DictionaryMineralAbundance() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
