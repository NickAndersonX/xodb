using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    public partial class DictionarySurfaceRoughness
    {
        public DictionarySurfaceRoughness(Session session) : base(session) { }
        public DictionarySurfaceRoughness() : base(Session.DefaultSession) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
