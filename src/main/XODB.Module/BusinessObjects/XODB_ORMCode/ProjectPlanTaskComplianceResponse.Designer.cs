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

    [Persistent(@"X_ProjectPlanTaskComplianceResponse")]
    public partial class ProjectPlanTaskComplianceResponse : XPLiteObject
    {
        Guid fProjectPlanTaskComplianceResponseID;
        [Key(true)]
        public Guid ProjectPlanTaskComplianceResponseID
        {
            get { return fProjectPlanTaskComplianceResponseID; }
            set { SetPropertyValue<Guid>("ProjectPlanTaskComplianceResponseID", ref fProjectPlanTaskComplianceResponseID, value); }
        }
        ProjectPlanTask fProjectPlanTaskID;
        [Association(@"ProjectPlanTaskComplianceResponseReferencesX_ProjectPlanTask")]
        public ProjectPlanTask ProjectPlanTaskID
        {
            get { return fProjectPlanTaskID; }
            set { SetPropertyValue<ProjectPlanTask>("ProjectPlanTaskID", ref fProjectPlanTaskID, value); }
        }
        ProjectPlanTaskResponse fProjectPlanTaskResponseID;
        [Association(@"ProjectPlanTaskComplianceResponseReferencesProjectPlanTaskResponse")]
        public ProjectPlanTaskResponse ProjectPlanTaskResponseID
        {
            get { return fProjectPlanTaskResponseID; }
            set { SetPropertyValue<ProjectPlanTaskResponse>("ProjectPlanTaskResponseID", ref fProjectPlanTaskResponseID, value); }
        }
        ComplianceWorkType fComplianceWorkTypeID;
        [Association(@"ProjectPlanTaskComplianceResponseReferencesComplianceWorkType")]
        public ComplianceWorkType ComplianceWorkTypeID
        {
            get { return fComplianceWorkTypeID; }
            set { SetPropertyValue<ComplianceWorkType>("ComplianceWorkTypeID", ref fComplianceWorkTypeID, value); }
        }
        string fComplianceFormResponse;
        [Size(SizeAttribute.Unlimited)]
        public string ComplianceFormResponse
        {
            get { return fComplianceFormResponse; }
            set { SetPropertyValue<string>("ComplianceFormResponse", ref fComplianceFormResponse, value); }
        }
        Contact fCompletedBy;
        [Association(@"ProjectPlanTaskComplianceResponseReferencesContact")]
        public Contact CompletedBy
        {
            get { return fCompletedBy; }
            set { SetPropertyValue<Contact>("CompletedBy", ref fCompletedBy, value); }
        }
        DateTime fCompleted;
        public DateTime Completed
        {
            get { return fCompleted; }
            set { SetPropertyValue<DateTime>("Completed", ref fCompleted, value); }
        }
        Contact fApprovedBy;
        [Association(@"ProjectPlanTaskComplianceResponseReferencesContact1")]
        public Contact ApprovedBy
        {
            get { return fApprovedBy; }
            set { SetPropertyValue<Contact>("ApprovedBy", ref fApprovedBy, value); }
        }
        DateTime fApproved;
        public DateTime Approved
        {
            get { return fApproved; }
            set { SetPropertyValue<DateTime>("Approved", ref fApproved, value); }
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
