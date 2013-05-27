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

    [Persistent(@"X_ProjectPlanTask")]
    public partial class ProjectPlanTask : XPLiteObject
    {
        Guid fProjectPlanTaskID;
        [Key(true)]
        public Guid ProjectPlanTaskID
        {
            get { return fProjectPlanTaskID; }
            set { SetPropertyValue<Guid>("ProjectPlanTaskID", ref fProjectPlanTaskID, value); }
        }
        ProjectPlan fProjectPlanID;
        [Association(@"X_ProjectPlanTaskReferencesProjectPlan")]
        public ProjectPlan ProjectPlanID
        {
            get { return fProjectPlanID; }
            set { SetPropertyValue<ProjectPlan>("ProjectPlanID", ref fProjectPlanID, value); }
        }
        ProjectPlanTask fParentProjectTaskID;
        [Association(@"X_ProjectPlanTaskReferencesX_ProjectPlanTask")]
        public ProjectPlanTask ParentProjectTaskID
        {
            get { return fParentProjectTaskID; }
            set { SetPropertyValue<ProjectPlanTask>("ParentProjectTaskID", ref fParentProjectTaskID, value); }
        }
        string fProjectTaskName;
        [Size(50)]
        public string ProjectTaskName
        {
            get { return fProjectTaskName; }
            set { SetPropertyValue<string>("ProjectTaskName", ref fProjectTaskName, value); }
        }
        DictionaryWorkType fWorkTypeID;
        [Association(@"X_ProjectPlanTaskReferencesX_DictionaryWorkType")]
        public DictionaryWorkType WorkTypeID
        {
            get { return fWorkTypeID; }
            set { SetPropertyValue<DictionaryWorkType>("WorkTypeID", ref fWorkTypeID, value); }
        }
        int fPriority;
        public int Priority
        {
            get { return fPriority; }
            set { SetPropertyValue<int>("Priority", ref fPriority, value); }
        }
        decimal fProgress;
        public decimal Progress
        {
            get { return fProgress; }
            set { SetPropertyValue<decimal>("Progress", ref fProgress, value); }
        }
        DateTime fStartDate;
        public DateTime StartDate
        {
            get { return fStartDate; }
            set { SetPropertyValue<DateTime>("StartDate", ref fStartDate, value); }
        }
        DateTime fStopDate;
        public DateTime StopDate
        {
            get { return fStopDate; }
            set { SetPropertyValue<DateTime>("StopDate", ref fStopDate, value); }
        }
        decimal fAllocatedManHours;
        public decimal AllocatedManHours
        {
            get { return fAllocatedManHours; }
            set { SetPropertyValue<decimal>("AllocatedManHours", ref fAllocatedManHours, value); }
        }
        string fComment;
        [Size(255)]
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
        [Association(@"ProjectPlanTaskResponseReferencesX_ProjectPlanTask", typeof(ProjectPlanTaskResponse))]
        public XPCollection<ProjectPlanTaskResponse> ProjectPlanTaskResponses { get { return GetCollection<ProjectPlanTaskResponse>("ProjectPlanTaskResponses"); } }
        [Association(@"ProjectPlanTaskWorkerReferencesX_ProjectPlanTask", typeof(ProjectPlanTaskWorker))]
        public XPCollection<ProjectPlanTaskWorker> ProjectPlanTaskWorkers { get { return GetCollection<ProjectPlanTaskWorker>("ProjectPlanTaskWorkers"); } }
        [Association(@"ProjectPlanTaskComplianceResponseReferencesX_ProjectPlanTask", typeof(ProjectPlanTaskComplianceResponse))]
        public XPCollection<ProjectPlanTaskComplianceResponse> ProjectPlanTaskComplianceResponses { get { return GetCollection<ProjectPlanTaskComplianceResponse>("ProjectPlanTaskComplianceResponses"); } }
        [Association(@"ProjectDeliverableReferencesX_ProjectPlanTask", typeof(ProjectDeliverable))]
        public XPCollection<ProjectDeliverable> ProjectDeliverables { get { return GetCollection<ProjectDeliverable>("ProjectDeliverables"); } }
        [Association(@"X_ProjectPlanTaskReferencesX_ProjectPlanTask", typeof(ProjectPlanTask))]
        public XPCollection<ProjectPlanTask> X_ProjectPlanTaskCollection { get { return GetCollection<ProjectPlanTask>("X_ProjectPlanTaskCollection"); } }
        [Association(@"Q_ProjectPlanTaskPartReferencesProjectPlanTask", typeof(ProjectPlanTaskPart))]
        public XPCollection<ProjectPlanTaskPart> Q_ProjectPlanTaskParts { get { return GetCollection<ProjectPlanTaskPart>("Q_ProjectPlanTaskParts"); } }
        [Association(@"Q_ProjectPlanTaskModelReferencesProjectPlanTask", typeof(ProjectPlanTaskModel))]
        public XPCollection<ProjectPlanTaskModel> Q_ProjectPlanTaskModels { get { return GetCollection<ProjectPlanTaskModel>("Q_ProjectPlanTaskModels"); } }
    }

}
