using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class ParameterDuplicate
    {
        public ParameterDuplicate(Session session) : base(session) { }
        public ParameterDuplicate() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
