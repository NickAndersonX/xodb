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

    [Persistent(@"X_Province")]
    public partial class Province : XPLiteObject
    {
        Guid fProvinceID;
        [Key(true)]
        public Guid ProvinceID
        {
            get { return fProvinceID; }
            set { SetPropertyValue<Guid>("ProvinceID", ref fProvinceID, value); }
        }
        string fProvinceName;
        [Size(200)]
        public string ProvinceName
        {
            get { return fProvinceName; }
            set { SetPropertyValue<string>("ProvinceName", ref fProvinceName, value); }
        }
        DictionaryProvinceType fProvinceTypeID;
        [Association(@"ProvinceReferencesDictionaryProvinceType")]
        public DictionaryProvinceType ProvinceTypeID
        {
            get { return fProvinceTypeID; }
            set { SetPropertyValue<DictionaryProvinceType>("ProvinceTypeID", ref fProvinceTypeID, value); }
        }
        DictionaryProvinceGroup fProvinceGroupID;
        [Indexed(Name = @"iProvinceGroupID_Province")]
        [Association(@"ProvinceReferencesDictionaryProvinceGroup")]
        public DictionaryProvinceGroup ProvinceGroupID
        {
            get { return fProvinceGroupID; }
            set { SetPropertyValue<DictionaryProvinceGroup>("ProvinceGroupID", ref fProvinceGroupID, value); }
        }
        DictionaryCountry fCountryID;
        [Size(2)]
        [Association(@"ProvinceReferencesDictionaryCountry")]
        public DictionaryCountry CountryID
        {
            get { return fCountryID; }
            set { SetPropertyValue<DictionaryCountry>("CountryID", ref fCountryID, value); }
        }
        DictionaryCountryState fCountryStateID;
        [Association(@"ProvinceReferencesDictionaryCountryState")]
        public DictionaryCountryState CountryStateID
        {
            get { return fCountryStateID; }
            set { SetPropertyValue<DictionaryCountryState>("CountryStateID", ref fCountryStateID, value); }
        }
        Company fOwnerCompanyID;
        [Association(@"ProvinceReferencesCompany")]
        public Company OwnerCompanyID
        {
            get { return fOwnerCompanyID; }
            set { SetPropertyValue<Company>("OwnerCompanyID", ref fOwnerCompanyID, value); }
        }
        Contact fApprovedByContactID;
        [Association(@"ProvinceReferencesContact")]
        public Contact ApprovedByContactID
        {
            get { return fApprovedByContactID; }
            set { SetPropertyValue<Contact>("ApprovedByContactID", ref fApprovedByContactID, value); }
        }
        int fVersion;
        public int Version
        {
            get { return fVersion; }
            set { SetPropertyValue<int>("Version", ref fVersion, value); }
        }
        Guid fVersionAntecedentID;
        public Guid VersionAntecedentID
        {
            get { return fVersionAntecedentID; }
            set { SetPropertyValue<Guid>("VersionAntecedentID", ref fVersionAntecedentID, value); }
        }
        int fVersionCertainty;
        public int VersionCertainty
        {
            get { return fVersionCertainty; }
            set { SetPropertyValue<int>("VersionCertainty", ref fVersionCertainty, value); }
        }
        Guid fVersionWorkflowInstanceID;
        public Guid VersionWorkflowInstanceID
        {
            get { return fVersionWorkflowInstanceID; }
            set { SetPropertyValue<Guid>("VersionWorkflowInstanceID", ref fVersionWorkflowInstanceID, value); }
        }
        Guid fVersionUpdatedBy;
        public Guid VersionUpdatedBy
        {
            get { return fVersionUpdatedBy; }
            set { SetPropertyValue<Guid>("VersionUpdatedBy", ref fVersionUpdatedBy, value); }
        }
        Guid fVersionDeletedBy;
        public Guid VersionDeletedBy
        {
            get { return fVersionDeletedBy; }
            set { SetPropertyValue<Guid>("VersionDeletedBy", ref fVersionDeletedBy, value); }
        }
        Guid fVersionOwnerContactID;
        public Guid VersionOwnerContactID
        {
            get { return fVersionOwnerContactID; }
            set { SetPropertyValue<Guid>("VersionOwnerContactID", ref fVersionOwnerContactID, value); }
        }
        Guid fVersionOwnerCompanyID;
        public Guid VersionOwnerCompanyID
        {
            get { return fVersionOwnerCompanyID; }
            set { SetPropertyValue<Guid>("VersionOwnerCompanyID", ref fVersionOwnerCompanyID, value); }
        }
        DateTime fVersionUpdated;
        public DateTime VersionUpdated
        {
            get { return fVersionUpdated; }
            set { SetPropertyValue<DateTime>("VersionUpdated", ref fVersionUpdated, value); }
        }
        [Association(@"ProspectusProjectProvinceReferencesProvince", typeof(ProspectusProjectProvince))]
        public XPCollection<ProspectusProjectProvince> ProspectusProjectProvinces { get { return GetCollection<ProspectusProjectProvince>("ProspectusProjectProvinces"); } }
        [Association(@"TenementProvinceReferencesProvince", typeof(TenementProvince))]
        public XPCollection<TenementProvince> TenementProvinces { get { return GetCollection<TenementProvince>("TenementProvinces"); } }
        [Association(@"TenementReferencesProvince", typeof(Tenement))]
        public XPCollection<Tenement> Tenements { get { return GetCollection<Tenement>("Tenements"); } }
        [Association(@"ComplianceReferencesProvince", typeof(Compliance))]
        public XPCollection<Compliance> Compliances { get { return GetCollection<Compliance>("Compliances"); } }
    }

}
