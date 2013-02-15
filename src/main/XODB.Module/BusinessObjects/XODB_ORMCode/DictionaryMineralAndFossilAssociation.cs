using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryMineralAndFossilAssociation
    {
        public DictionaryMineralAndFossilAssociation(Session session) : base(session) { }
        public DictionaryMineralAndFossilAssociation() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
