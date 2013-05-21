using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ModelScheduleCycle
    {
        public ModelScheduleCycle(Session session) : base(session) { }
        public ModelScheduleCycle() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
