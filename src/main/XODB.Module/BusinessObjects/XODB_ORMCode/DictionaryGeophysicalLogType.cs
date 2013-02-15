using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryGeophysicalLogType
    {
        public DictionaryGeophysicalLogType(Session session) : base(session) { }
        public DictionaryGeophysicalLogType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
