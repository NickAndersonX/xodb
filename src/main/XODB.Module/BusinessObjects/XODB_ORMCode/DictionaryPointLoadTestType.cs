using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryPointLoadTestType
    {
        public DictionaryPointLoadTestType(Session session) : base(session) { }
        public DictionaryPointLoadTestType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
