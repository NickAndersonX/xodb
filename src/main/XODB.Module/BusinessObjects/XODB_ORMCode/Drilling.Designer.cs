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

    [Persistent(@"X_Drilling")]
    public partial class Drilling : XPLiteObject
    {
        Guid fDrillingID;
        [Key(true)]
        public Guid DrillingID
        {
            get { return fDrillingID; }
            set { SetPropertyValue<Guid>("DrillingID", ref fDrillingID, value); }
        }
        Header fHeaderID;
        [Association(@"DrillingReferencesHeader")]
        public Header HeaderID
        {
            get { return fHeaderID; }
            set { SetPropertyValue<Header>("HeaderID", ref fHeaderID, value); }
        }
        decimal fFromDepth;
        public decimal FromDepth
        {
            get { return fFromDepth; }
            set { SetPropertyValue<decimal>("FromDepth", ref fFromDepth, value); }
        }
        decimal fToDepth;
        public decimal ToDepth
        {
            get { return fToDepth; }
            set { SetPropertyValue<decimal>("ToDepth", ref fToDepth, value); }
        }
        int fRunNumber;
        public int RunNumber
        {
            get { return fRunNumber; }
            set { SetPropertyValue<int>("RunNumber", ref fRunNumber, value); }
        }
        decimal fRecoveredThickness;
        public decimal RecoveredThickness
        {
            get { return fRecoveredThickness; }
            set { SetPropertyValue<decimal>("RecoveredThickness", ref fRecoveredThickness, value); }
        }
        DateTime fDrillingDate;
        public DateTime DrillingDate
        {
            get { return fDrillingDate; }
            set { SetPropertyValue<DateTime>("DrillingDate", ref fDrillingDate, value); }
        }
        Company fDrillingCompanyID;
        [Association(@"DrillingReferencesCompany")]
        public Company DrillingCompanyID
        {
            get { return fDrillingCompanyID; }
            set { SetPropertyValue<Company>("DrillingCompanyID", ref fDrillingCompanyID, value); }
        }
        string fDrillRigNumber;
        [Size(16)]
        public string DrillRigNumber
        {
            get { return fDrillRigNumber; }
            set { SetPropertyValue<string>("DrillRigNumber", ref fDrillRigNumber, value); }
        }
        DictionaryDrillRigType fDrillRigTypeID;
        [Association(@"DrillingReferencesDictionaryDrillRigType")]
        public DictionaryDrillRigType DrillRigTypeID
        {
            get { return fDrillRigTypeID; }
            set { SetPropertyValue<DictionaryDrillRigType>("DrillRigTypeID", ref fDrillRigTypeID, value); }
        }
        Contact fDrillerID;
        [Association(@"DrillingReferencesContact")]
        public Contact DrillerID
        {
            get { return fDrillerID; }
            set { SetPropertyValue<Contact>("DrillerID", ref fDrillerID, value); }
        }
        DictionaryDrillBitType fDrillBitTypeID;
        [Association(@"DrillingReferencesDictionaryDrillBitType")]
        public DictionaryDrillBitType DrillBitTypeID
        {
            get { return fDrillBitTypeID; }
            set { SetPropertyValue<DictionaryDrillBitType>("DrillBitTypeID", ref fDrillBitTypeID, value); }
        }
        DictionaryDrillFluid fDrillFluidID;
        [Association(@"DrillingReferencesDictionaryDrillFluid")]
        public DictionaryDrillFluid DrillFluidID
        {
            get { return fDrillFluidID; }
            set { SetPropertyValue<DictionaryDrillFluid>("DrillFluidID", ref fDrillFluidID, value); }
        }
        DictionaryDrillSize fDrillSizeID;
        [Association(@"DrillingReferencesDictionaryDrillSize")]
        public DictionaryDrillSize DrillSizeID
        {
            get { return fDrillSizeID; }
            set { SetPropertyValue<DictionaryDrillSize>("DrillSizeID", ref fDrillSizeID, value); }
        }
        int fCoreSize;
        public int CoreSize
        {
            get { return fCoreSize; }
            set { SetPropertyValue<int>("CoreSize", ref fCoreSize, value); }
        }
        int fHoleSize;
        public int HoleSize
        {
            get { return fHoleSize; }
            set { SetPropertyValue<int>("HoleSize", ref fHoleSize, value); }
        }
        bool fReamed;
        public bool Reamed
        {
            get { return fReamed; }
            set { SetPropertyValue<bool>("Reamed", ref fReamed, value); }
        }
        string fComment;
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get { return fComment; }
            set { SetPropertyValue<string>("Comment", ref fComment, value); }
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
        [Association(@"CompositeReferencesDrilling", typeof(Composite))]
        public XPCollection<Composite> Composites { get { return GetCollection<Composite>("Composites"); } }
    }

}
