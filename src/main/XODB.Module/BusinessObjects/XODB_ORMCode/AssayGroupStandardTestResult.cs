using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class AssayGroupStandardTestResult
    {
        public AssayGroupStandardTestResult(Session session) : base(session) { }
        public AssayGroupStandardTestResult() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
