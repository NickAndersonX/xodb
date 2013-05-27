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

    [Persistent(@"E_Journal")]
    public partial class Journal : XPLiteObject
    {
        Guid fJournalID;
        [Key(true)]
        public Guid JournalID
        {
            get { return fJournalID; }
            set { SetPropertyValue<Guid>("JournalID", ref fJournalID, value); }
        }
        string fReferenceType;
        [Size(60)]
        public string ReferenceType
        {
            get { return fReferenceType; }
            set { SetPropertyValue<string>("ReferenceType", ref fReferenceType, value); }
        }
        Guid fReferenceID;
        public Guid ReferenceID
        {
            get { return fReferenceID; }
            set { SetPropertyValue<Guid>("ReferenceID", ref fReferenceID, value); }
        }
        DateTime fReferenceDate;
        public DateTime ReferenceDate
        {
            get { return fReferenceDate; }
            set { SetPropertyValue<DateTime>("ReferenceDate", ref fReferenceDate, value); }
        }
        Account fAccountID;
        [Association(@"E_JournalReferencesQ_Account")]
        public Account AccountID
        {
            get { return fAccountID; }
            set { SetPropertyValue<Account>("AccountID", ref fAccountID, value); }
        }
        decimal fAmount;
        public decimal Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<decimal>("Amount", ref fAmount, value); }
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
        decimal fOriginalAmount;
        public decimal OriginalAmount
        {
            get { return fOriginalAmount; }
            set { SetPropertyValue<decimal>("OriginalAmount", ref fOriginalAmount, value); }
        }
        Currency fCurrencyID;
        [Association(@"JournalReferencesE_Currency")]
        public Currency CurrencyID
        {
            get { return fCurrencyID; }
            set { SetPropertyValue<Currency>("CurrencyID", ref fCurrencyID, value); }
        }
    }

}
