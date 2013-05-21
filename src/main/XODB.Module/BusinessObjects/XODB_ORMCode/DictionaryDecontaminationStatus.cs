using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDecontaminationStatus
    {
        public DictionaryDecontaminationStatus(Session session) : base(session) { }
        public DictionaryDecontaminationStatus() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
