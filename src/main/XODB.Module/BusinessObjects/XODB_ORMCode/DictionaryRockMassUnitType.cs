using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryRockMassUnitType
    {
        public DictionaryRockMassUnitType(Session session) : base(session) { }
        public DictionaryRockMassUnitType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
