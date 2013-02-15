using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryIntervalStatus
    {
        public DictionaryIntervalStatus(Session session) : base(session) { }
        public DictionaryIntervalStatus() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
