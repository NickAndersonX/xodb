//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XODB.Module.BusinessObjects.XODB
{

    [Persistent(@"X_DictionaryGeophysicalLogType")]
    public partial class DictionaryGeophysicalLogType : XPLiteObject
    {
        Guid fGeophysicalLogTypeID;
        [Key(true)]
        public Guid GeophysicalLogTypeID
        {
            get { return fGeophysicalLogTypeID; }
            set { SetPropertyValue<Guid>("GeophysicalLogTypeID", ref fGeophysicalLogTypeID, value); }
        }
        string fStandardGeophysicalLogTypeName;
        [Indexed(Name = @"IDictionaryGeophysicalLogType", Unique = true)]
        [Size(16)]
        public string StandardGeophysicalLogTypeName
        {
            get { return fStandardGeophysicalLogTypeName; }
            set { SetPropertyValue<string>("StandardGeophysicalLogTypeName", ref fStandardGeophysicalLogTypeName, value); }
        }
        string fCoalGeophysicalLogTypeName;
        [Size(16)]
        public string CoalGeophysicalLogTypeName
        {
            get { return fCoalGeophysicalLogTypeName; }
            set { SetPropertyValue<string>("CoalGeophysicalLogTypeName", ref fCoalGeophysicalLogTypeName, value); }
        }
        string fGoldGeophysicalLogTypeName;
        [Size(16)]
        public string GoldGeophysicalLogTypeName
        {
            get { return fGoldGeophysicalLogTypeName; }
            set { SetPropertyValue<string>("GoldGeophysicalLogTypeName", ref fGoldGeophysicalLogTypeName, value); }
        }
        string fCustomGeophysicalLogTypeName;
        [Size(16)]
        public string CustomGeophysicalLogTypeName
        {
            get { return fCustomGeophysicalLogTypeName; }
            set { SetPropertyValue<string>("CustomGeophysicalLogTypeName", ref fCustomGeophysicalLogTypeName, value); }
        }
        string fDescription;
        [Size(255)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        string fFullDescription;
        [Size(SizeAttribute.Unlimited)]
        public string FullDescription
        {
            get { return fFullDescription; }
            set { SetPropertyValue<string>("FullDescription", ref fFullDescription, value); }
        }
        byte[] fThumbnail;
        [Size(SizeAttribute.Unlimited)]
        public byte[] Thumbnail
        {
            get { return fThumbnail; }
            set { SetPropertyValue<byte[]>("Thumbnail", ref fThumbnail, value); }
        }
        FileData fOriginalFileDataID;
        [Association(@"DictionaryGeophysicalLogTypeReferencesFileData")]
        public FileData OriginalFileDataID
        {
            get { return fOriginalFileDataID; }
            set { SetPropertyValue<FileData>("OriginalFileDataID", ref fOriginalFileDataID, value); }
        }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType", typeof(Header))]
        public XPCollection<Header> Headers { get { return GetCollection<Header>("Headers"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType1", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders10 { get { return GetCollection<Header>("GeophysicalLogHeaders10"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType2", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders8 { get { return GetCollection<Header>("GeophysicalLogHeaders8"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType3", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders9 { get { return GetCollection<Header>("GeophysicalLogHeaders9"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType4", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders11 { get { return GetCollection<Header>("GeophysicalLogHeaders11"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType5", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders12 { get { return GetCollection<Header>("GeophysicalLogHeaders12"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType6", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders2 { get { return GetCollection<Header>("GeophysicalLogHeaders2"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType7", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders3 { get { return GetCollection<Header>("GeophysicalLogHeaders3"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType8", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders4 { get { return GetCollection<Header>("GeophysicalLogHeaders4"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType9", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders5 { get { return GetCollection<Header>("GeophysicalLogHeaders5"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType10", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders6 { get { return GetCollection<Header>("GeophysicalLogHeaders6"); } }
        [Association(@"HeaderReferencesDictionaryGeophysicalLogType11", typeof(Header))]
        public XPCollection<Header> GeophysicalLogHeaders7 { get { return GetCollection<Header>("GeophysicalLogHeaders7"); } }
    }

}
