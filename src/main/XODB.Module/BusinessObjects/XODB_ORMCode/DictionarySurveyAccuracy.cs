using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionarySurveyAccuracy
    {
        public DictionarySurveyAccuracy(Session session) : base(session) { }
        public DictionarySurveyAccuracy() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
