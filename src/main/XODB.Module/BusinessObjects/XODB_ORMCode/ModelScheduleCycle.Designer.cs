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

    [Persistent(@"Q_ModelScheduleCycle")]
    public partial class ModelScheduleCycle : XPLiteObject
    {
        Guid fModelScheduleCycleID;
        [Key(true)]
        public Guid ModelScheduleCycleID
        {
            get { return fModelScheduleCycleID; }
            set { SetPropertyValue<Guid>("ModelScheduleCycleID", ref fModelScheduleCycleID, value); }
        }
        DictionaryModel fModelID;
        [Association(@"ModelScheduleCycleReferencesX_DictionaryModel")]
        public DictionaryModel ModelID
        {
            get { return fModelID; }
            set { SetPropertyValue<DictionaryModel>("ModelID", ref fModelID, value); }
        }
        ModelProcedure fModelProcedureID;
        [Association(@"ModelScheduleCycleReferencesQ_ModelProcedure")]
        public ModelProcedure ModelProcedureID
        {
            get { return fModelProcedureID; }
            set { SetPropertyValue<ModelProcedure>("ModelProcedureID", ref fModelProcedureID, value); }
        }
        int fSequence;
        public int Sequence
        {
            get { return fSequence; }
            set { SetPropertyValue<int>("Sequence", ref fSequence, value); }
        }
        DictionaryUnit fScheduleFrequencyUnitID;
        [Association(@"ModelScheduleCycleReferencesDictionaryUnit")]
        public DictionaryUnit ScheduleFrequencyUnitID
        {
            get { return fScheduleFrequencyUnitID; }
            set { SetPropertyValue<DictionaryUnit>("ScheduleFrequencyUnitID", ref fScheduleFrequencyUnitID, value); }
        }
        decimal fScheduleFrequency;
        public decimal ScheduleFrequency
        {
            get { return fScheduleFrequency; }
            set { SetPropertyValue<decimal>("ScheduleFrequency", ref fScheduleFrequency, value); }
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