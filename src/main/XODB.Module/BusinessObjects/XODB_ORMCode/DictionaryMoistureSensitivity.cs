using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryMoistureSensitivity
    {
        public DictionaryMoistureSensitivity(Session session) : base(session) { }
        public DictionaryMoistureSensitivity() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
