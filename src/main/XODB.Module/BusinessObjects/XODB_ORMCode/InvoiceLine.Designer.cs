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

    [Persistent(@"E_InvoiceLine")]
    public partial class InvoiceLine : XPLiteObject
    {
        Guid fInvoiceLineID;
        [Key(true)]
        public Guid InvoiceLineID
        {
            get { return fInvoiceLineID; }
            set { SetPropertyValue<Guid>("InvoiceLineID", ref fInvoiceLineID, value); }
        }
        Invoice fInvoiceID;
        [Association(@"E_InvoiceLineReferencesE_Invoice")]
        public Invoice InvoiceID
        {
            get { return fInvoiceID; }
            set { SetPropertyValue<Invoice>("InvoiceID", ref fInvoiceID, value); }
        }
        SupplyItem fSupplyItemID;
        [Association(@"InvoiceLineReferencesE_SupplyItem")]
        public SupplyItem SupplyItemID
        {
            get { return fSupplyItemID; }
            set { SetPropertyValue<SupplyItem>("SupplyItemID", ref fSupplyItemID, value); }
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
        decimal fQuantity;
        public decimal Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<decimal>("Quantity", ref fQuantity, value); }
        }
        DictionaryTax fTaxID;
        [Association(@"E_InvoiceLineReferencesE_DictionaryTax")]
        public DictionaryTax TaxID
        {
            get { return fTaxID; }
            set { SetPropertyValue<DictionaryTax>("TaxID", ref fTaxID, value); }
        }
        decimal fTax;
        public decimal Tax
        {
            get { return fTax; }
            set { SetPropertyValue<decimal>("Tax", ref fTax, value); }
        }
        decimal fSubtotal;
        public decimal Subtotal
        {
            get { return fSubtotal; }
            set { SetPropertyValue<decimal>("Subtotal", ref fSubtotal, value); }
        }
        string fDescription;
        [Size(60)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
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
        decimal fOriginalSubtotal;
        public decimal OriginalSubtotal
        {
            get { return fOriginalSubtotal; }
            set { SetPropertyValue<decimal>("OriginalSubtotal", ref fOriginalSubtotal, value); }
        }
        Currency fCurrencyID;
        [Association(@"InvoiceLineReferencesE_Currency")]
        public Currency CurrencyID
        {
            get { return fCurrencyID; }
            set { SetPropertyValue<Currency>("CurrencyID", ref fCurrencyID, value); }
        }
        [Association(@"E_CreditReferencesInvoiceLine", typeof(Credit))]
        public XPCollection<Credit> E_Credits { get { return GetCollection<Credit>("E_Credits"); } }
    }

}
