using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryLithologyInterrelation
    {
        public DictionaryLithologyInterrelation(Session session) : base(session) { }
        public DictionaryLithologyInterrelation() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
