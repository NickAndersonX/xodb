using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryInfillType
    {
        public DictionaryInfillType(Session session) : base(session) { }
        public DictionaryInfillType() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
