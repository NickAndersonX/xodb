using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDefectCompleteness
    {
        public DictionaryDefectCompleteness(Session session) : base(session) { }
        public DictionaryDefectCompleteness() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
