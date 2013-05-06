using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryWorkTypeClass
    {
        public DictionaryWorkTypeClass(Session session) : base(session) { }
        public DictionaryWorkTypeClass() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
