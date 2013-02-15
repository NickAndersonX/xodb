using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryProvinceGroup
    {
        public DictionaryProvinceGroup(Session session) : base(session) { }
        public DictionaryProvinceGroup() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
