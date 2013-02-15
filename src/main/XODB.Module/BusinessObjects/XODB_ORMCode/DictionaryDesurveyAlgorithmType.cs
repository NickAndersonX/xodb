using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDesurveyAlgorithmType
    {
        public DictionaryDesurveyAlgorithmType(Session session) : base(session) { }
        public DictionaryDesurveyAlgorithmType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
