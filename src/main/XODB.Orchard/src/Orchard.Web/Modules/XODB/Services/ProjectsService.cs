using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Ionic.Zip;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.FileSystems.Media;
using Orchard.Localization;
using XODB.Models;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Validation;
using Orchard;
using System.Transactions;
using XODB.ViewModels;

namespace XODB.Services {

    [UsedImplicitly]
    public class ProjectsService : IProjectsService {
        private readonly IOrchardServices _orchardServices;
        private readonly IUsersService _userServices;
        public ProjectsService(IOrchardServices orchardServices, IUsersService userService)
        {
            _userServices = userService;
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void CreateProject(Project o)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var p = new ProjectsDataContext();
                o.ProjectName = (o.ProjectName == "null") ? null : o.ProjectName.Substring(1, o.ProjectName.Length - 2);
                o.Comment = (o.Comment == "null") ? null : o.Comment.Substring(1, o.Comment.Length - 2);
                o.ProjectID = Guid.NewGuid();
                p.Projects.InsertOnSubmit(o);
                p.SubmitChanges();
            }

        }

        public void UpdateProject(ProjectViewModel m)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var now = DateTime.UtcNow;
                var d = new ProjectsDataContext();
                var p = new Project();
                p.ProjectName = (m.ProjectName == "null") ? null : m.ProjectName;
                p.Comment = (m.Comment == "null") ? null : m.Comment;
                p.ProjectID = Guid.NewGuid();
                p.VersionOwnerContactID = m.Creator;
                p.VersionUpdated = now;
                p.VersionUpdatedBy = m.User;
                d.Projects.InsertOnSubmit(p);
                var n = new ProjectPlan();
                n.ProjectPlanID = Guid.NewGuid();
                n.ProjectID = p.ProjectID;
                n.ProjectPlanName = "Resource Modelling";
                n.ResponsibleContactID = m.Creator;
                n.VersionOwnerContactID = m.User;
                n.VersionUpdated = now;
                n.VersionUpdatedBy = m.User;
                d.ProjectPlans.InsertOnSubmit(n);
                var t = new ProjectPlanTask();
                t.ProjectPlanTaskID = Guid.NewGuid();
                t.ProjectPlanID = n.ProjectPlanID;
                t.ProjectTaskName = m.StageName;
                t.VersionOwnerContactID = m.Creator;
                t.VersionUpdated = now;
                t.VersionUpdatedBy = m.User;
                d.ProjectPlanTasks.InsertOnSubmit(t);
                var tr = new ProjectPlanTask();
                tr.ProjectPlanTaskID = Guid.NewGuid();
                tr.ProjectPlanID = n.ProjectPlanID;
                tr.ProjectTaskName = "Review";
                tr.VersionOwnerContactID = m.Reviewer;
                tr.VersionUpdated = now.AddSeconds(-1); //This is not the current task
                tr.VersionUpdatedBy = m.User;
                d.ProjectPlanTasks.InsertOnSubmit(tr);
                var tc = new ProjectPlanTask();
                tc.ProjectPlanTaskID = Guid.NewGuid();
                tc.ProjectPlanID = n.ProjectPlanID;
                tc.ProjectTaskName = "Complete";
                tc.VersionOwnerContactID = m.User;
                tc.VersionUpdated = now.AddSeconds(-1); //This is not the current task
                tc.VersionUpdatedBy = m.User;
                d.ProjectPlanTasks.InsertOnSubmit(tr);
                d.SubmitChanges();
            }
        }

   
        public IEnumerable<Project> GetProjects() {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dataContext = new ProjectsDataContext();
                return dataContext.Projects.ToArray();
            }
        }

        public IEnumerable<ProjectPlanTask> GetStages(Guid ProjectID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var dataContext = new ProjectsDataContext();
                return dataContext.ProjectPlanTasks.Where(x=>x.ProjectPlan.ProjectID == ProjectID).ToArray();
            }
        }

        public void DeleteProject(Guid projectID, Guid contactID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ProjectsDataContext();
                var o = d.Projects.Where(x => x.ProjectID == projectID).Single();
                o.VersionDeletedBy  = contactID;
                o.VersionUpdated = DateTime.UtcNow;
                d.SubmitChanges();
            }
        }

        public void EmailAllProjectOwners(string subject, string body)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ProjectsDataContext();
                var o = d.Projects.Where(x => x.VersionOwnerContactID != null).Select(f=>(Guid)f.VersionOwnerContactID).ToArray();
                _userServices.EmailUsers(_userServices.GetUserEmails(o), subject, body);
            }
        }

    }
}
