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

    [Persistent(@"X_DictionaryWorkTypeClass")]
    public partial class DictionaryWorkTypeClass : XPLiteObject
    {
        Guid fWorkTypeClassID;
        [Key(true)]
        public Guid WorkTypeClassID
        {
            get { return fWorkTypeClassID; }
            set { SetPropertyValue<Guid>("WorkTypeClassID", ref fWorkTypeClassID, value); }
        }
        string fStandardWorkTypeClassName;
        [Size(16)]
        public string StandardWorkTypeClassName
        {
            get { return fStandardWorkTypeClassName; }
            set { SetPropertyValue<string>("StandardWorkTypeClassName", ref fStandardWorkTypeClassName, value); }
        }
        string fCoalWorkTypeClassName;
        [Size(16)]
        public string CoalWorkTypeClassName
        {
            get { return fCoalWorkTypeClassName; }
            set { SetPropertyValue<string>("CoalWorkTypeClassName", ref fCoalWorkTypeClassName, value); }
        }
        string fGoldWorkTypeClassName;
        [Size(16)]
        public string GoldWorkTypeClassName
        {
            get { return fGoldWorkTypeClassName; }
            set { SetPropertyValue<string>("GoldWorkTypeClassName", ref fGoldWorkTypeClassName, value); }
        }
        string fCustomWorkTypeClassName;
        [Size(16)]
        public string CustomWorkTypeClassName
        {
            get { return fCustomWorkTypeClassName; }
            set { SetPropertyValue<string>("CustomWorkTypeClassName", ref fCustomWorkTypeClassName, value); }
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
        [Association(@"DictionaryWorkTypeClassReferencesFileData")]
        public FileData OriginalFileDataID
        {
            get { return fOriginalFileDataID; }
            set { SetPropertyValue<FileData>("OriginalFileDataID", ref fOriginalFileDataID, value); }
        }
    }

}
