using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class GeophysicalLoggingCompany
    {
        public GeophysicalLoggingCompany(Session session) : base(session) { }
        public GeophysicalLoggingCompany() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
