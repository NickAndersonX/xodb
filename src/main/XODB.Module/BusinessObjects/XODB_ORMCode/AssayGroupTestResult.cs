using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class AssayGroupTestResult
    {
        public AssayGroupTestResult(Session session) : base(session) { }
        public AssayGroupTestResult() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
