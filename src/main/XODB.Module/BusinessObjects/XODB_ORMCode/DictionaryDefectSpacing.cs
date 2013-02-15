using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDefectSpacing
    {
        public DictionaryDefectSpacing(Session session) : base(session) { }
        public DictionaryDefectSpacing() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
