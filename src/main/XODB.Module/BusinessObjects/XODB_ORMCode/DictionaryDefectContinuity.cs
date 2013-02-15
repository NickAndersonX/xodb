using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDefectContinuity
    {
        public DictionaryDefectContinuity(Session session) : base(session) { }
        public DictionaryDefectContinuity() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
