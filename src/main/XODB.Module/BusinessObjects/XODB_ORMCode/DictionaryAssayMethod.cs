using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryAssayMethod
    {
        public DictionaryAssayMethod(Session session) : base(session) { }
        public DictionaryAssayMethod() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
