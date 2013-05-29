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

    [Persistent(@"Q_ContractAsset")]
    public partial class ContractAsset : XPLiteObject
    {
        Guid fContractAssetID;
        [Key(true)]
        public Guid ContractAssetID
        {
            get { return fContractAssetID; }
            set { SetPropertyValue<Guid>("ContractAssetID", ref fContractAssetID, value); }
        }
        Contract fContractID;
        [Association(@"ContractAssetReferencesContract")]
        public Contract ContractID
        {
            get { return fContractID; }
            set { SetPropertyValue<Contract>("ContractID", ref fContractID, value); }
        }
        Asset fAssetID;
        [Association(@"ContractAssetReferencesQ_Asset")]
        public Asset AssetID
        {
            get { return fAssetID; }
            set { SetPropertyValue<Asset>("AssetID", ref fAssetID, value); }
        }
        DateTime fOverrideCoverageStart;
        public DateTime OverrideCoverageStart
        {
            get { return fOverrideCoverageStart; }
            set { SetPropertyValue<DateTime>("OverrideCoverageStart", ref fOverrideCoverageStart, value); }
        }
        DateTime fOverrideCoverageEnd;
        public DateTime OverrideCoverageEnd
        {
            get { return fOverrideCoverageEnd; }
            set { SetPropertyValue<DateTime>("OverrideCoverageEnd", ref fOverrideCoverageEnd, value); }
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
    }

}