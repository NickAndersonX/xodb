using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryWorkTypeRelation
    {
        public DictionaryWorkTypeRelation(Session session) : base(session) { }
        public DictionaryWorkTypeRelation() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
