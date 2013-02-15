using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionaryGeodeticDatum
    {
        public DictionaryGeodeticDatum(Session session) : base(session) { }
        public DictionaryGeodeticDatum() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
