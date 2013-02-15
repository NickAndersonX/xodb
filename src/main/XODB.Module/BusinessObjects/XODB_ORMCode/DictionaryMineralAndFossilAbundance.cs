using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryMineralAndFossilAbundance
    {
        public DictionaryMineralAndFossilAbundance(Session session) : base(session) { }
        public DictionaryMineralAndFossilAbundance() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
