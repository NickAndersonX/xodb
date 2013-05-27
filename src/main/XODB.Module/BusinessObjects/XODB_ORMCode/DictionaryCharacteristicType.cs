using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryCharacteristicType
    {
        public DictionaryCharacteristicType(Session session) : base(session) { }
        public DictionaryCharacteristicType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
