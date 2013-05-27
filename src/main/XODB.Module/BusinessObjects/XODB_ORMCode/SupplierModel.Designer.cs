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

    [Persistent(@"Q_SupplierModel")]
    public partial class SupplierModel : XPLiteObject
    {
        Guid fSupplierModelID;
        [Key(true)]
        public Guid SupplierModelID
        {
            get { return fSupplierModelID; }
            set { SetPropertyValue<Guid>("SupplierModelID", ref fSupplierModelID, value); }
        }
        Company fSupplierID;
        [Association(@"Q_SupplierModelReferencesCompany")]
        public Company SupplierID
        {
            get { return fSupplierID; }
            set { SetPropertyValue<Company>("SupplierID", ref fSupplierID, value); }
        }
        DictionaryModel fModelID;
        [Association(@"SupplierModelReferencesX_DictionaryModel")]
        public DictionaryModel ModelID
        {
            get { return fModelID; }
            set { SetPropertyValue<DictionaryModel>("ModelID", ref fModelID, value); }
        }
        int fSeqeunce;
        public int Seqeunce
        {
            get { return fSeqeunce; }
            set { SetPropertyValue<int>("Seqeunce", ref fSeqeunce, value); }
        }
        Contract fSupplierContractID;
        [Association(@"Q_SupplierModelReferencesContract")]
        public Contract SupplierContractID
        {
            get { return fSupplierContractID; }
            set { SetPropertyValue<Contract>("SupplierContractID", ref fSupplierContractID, value); }
        }
        bool fIsOEM;
        public bool IsOEM
        {
            get { return fIsOEM; }
            set { SetPropertyValue<bool>("IsOEM", ref fIsOEM, value); }
        }
        decimal fOrderQuantityMinimum;
        public decimal OrderQuantityMinimum
        {
            get { return fOrderQuantityMinimum; }
            set { SetPropertyValue<decimal>("OrderQuantityMinimum", ref fOrderQuantityMinimum, value); }
        }
        decimal fPricePerUnit;
        public decimal PricePerUnit
        {
            get { return fPricePerUnit; }
            set { SetPropertyValue<decimal>("PricePerUnit", ref fPricePerUnit, value); }
        }
        DictionaryUnit fPriceUnitID;
        [Association(@"Q_SupplierModelReferencesDictionaryUnit")]
        public DictionaryUnit PriceUnitID
        {
            get { return fPriceUnitID; }
            set { SetPropertyValue<DictionaryUnit>("PriceUnitID", ref fPriceUnitID, value); }
        }
        string fSupplierModelNumber;
        [Size(60)]
        public string SupplierModelNumber
        {
            get { return fSupplierModelNumber; }
            set { SetPropertyValue<string>("SupplierModelNumber", ref fSupplierModelNumber, value); }
        }
        string fSupplierModelDescription;
        [Size(SizeAttribute.Unlimited)]
        public string SupplierModelDescription
        {
            get { return fSupplierModelDescription; }
            set { SetPropertyValue<string>("SupplierModelDescription", ref fSupplierModelDescription, value); }
        }
        FileData fSupplierFileDataID;
        [Association(@"Q_SupplierModelReferencesFileData")]
        public FileData SupplierFileDataID
        {
            get { return fSupplierFileDataID; }
            set { SetPropertyValue<FileData>("SupplierFileDataID", ref fSupplierFileDataID, value); }
        }
        DictionaryUnit fLeadTimeUnitID;
        [Association(@"Q_SupplierModelReferencesDictionaryUnit1")]
        public DictionaryUnit LeadTimeUnitID
        {
            get { return fLeadTimeUnitID; }
            set { SetPropertyValue<DictionaryUnit>("LeadTimeUnitID", ref fLeadTimeUnitID, value); }
        }
        decimal fLeadTime;
        public decimal LeadTime
        {
            get { return fLeadTime; }
            set { SetPropertyValue<decimal>("LeadTime", ref fLeadTime, value); }
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
        [Association(@"Q_WarehouseModelSupplierReferencesQ_SupplierModel", typeof(WarehouseModelSupplier))]
        public XPCollection<WarehouseModelSupplier> Q_WarehouseModelSuppliers { get { return GetCollection<WarehouseModelSupplier>("Q_WarehouseModelSuppliers"); } }
        [Association(@"Q_ProjectPlanTaskModelReferencesQ_SupplierModel", typeof(ProjectPlanTaskModel))]
        public XPCollection<ProjectPlanTaskModel> Q_ProjectPlanTaskModels { get { return GetCollection<ProjectPlanTaskModel>("Q_ProjectPlanTaskModels"); } }
    }

}
