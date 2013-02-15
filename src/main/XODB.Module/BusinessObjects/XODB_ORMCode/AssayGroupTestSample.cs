using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class AssayGroupTestSample
    {
        public AssayGroupTestSample(Session session) : base(session) { }
        public AssayGroupTestSample() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
