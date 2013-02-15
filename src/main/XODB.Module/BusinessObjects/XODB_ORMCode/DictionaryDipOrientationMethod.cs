using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryDipOrientationMethod
    {
        public DictionaryDipOrientationMethod(Session session) : base(session) { }
        public DictionaryDipOrientationMethod() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
