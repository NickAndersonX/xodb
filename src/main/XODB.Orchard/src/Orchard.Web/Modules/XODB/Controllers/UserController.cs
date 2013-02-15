using System;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Localization;
using Orchard;
using XODB.Models;
using Orchard.Themes;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Mvc;
using DevExpress.Data;
using Orchard.Logging;
using Orchard.Core.Contents.Controllers;
using Orchard.Mvc;
using XODB.ViewModels;
using XODB.Services;
using XODB.Helpers;
using XODB.Reports;
using System.Threading.Tasks;
using ImpromptuInterface;

//TODO: Get all the transaction scopes out and put them in services
namespace XODB.Controllers {

    [Themed]
    public class UserController : Controller {
        public string Name { get { return "User"; } }
        public IOrchardServices Services { get; set; }
        public IBlockModelService BlockModelService { get; set; }
        public IProjectsService ProjectService { get; set; }
        public IParametersService ParameterService { get; set; }
        public IUsersService UserService { get; set; }
        public ILogger Logger { get; set; }
        public Localizer T { get; set; }
        public IPrivateDataService PrivateService { get; set; }
        public UserController(
            IOrchardServices services, 
            IBlockModelService blockModelService, 
            IProjectsService projectService, 
            IParametersService parameterService,
            IUsersService userService,
            IPrivateDataService privateService
            ) {
            
            Services = services;
            UserService = userService;
            BlockModelService = blockModelService;
            ParameterService = parameterService;
            ProjectService = projectService;
            PrivateService = privateService;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
            
        }

        private Guid getCurrentUserID()
        {
            return UserService.GetUserID(Services.WorkContext.CurrentUser.UserName);
        }

        [HttpGet]
        public ActionResult CompareModel()
        {
            var model = new BlockModelCompareViewModel
            {
                Models = BlockModelService.GetModelList(),
                Report = AllReports.GetReport(AllReports.ReportType.CompareModel)
            };
            model.ParametersModel1 = model.Models.Any() ? BlockModelService.GetModelParameterList(new Guid(model.Models.First().Value)) : null;
            model.DomainsModel1 = model.Models.Any() ? BlockModelService.GetModelDomainsList(new Guid(model.Models.First().Value)) : null;
            model.DomainsModel2 = model.DomainsModel1;
            model.ParametersModel2 = model.ParametersModel1;
            model.ParametersIntersectionBothModels = model.ParametersModel1;
            return View(model);
        }

        [HttpGet]
        public ActionResult GetModelDomains(string modelID)
        {
            return Json(BlockModelService.GetModelDomainsList(new Guid(modelID))
                , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetModelParameters(string modelID)
        {
            return Json(BlockModelService.GetModelParameterList(new Guid(modelID))
                , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetModelIntersectionParameters(string modelID1, string modelID2)
        {
            
            return Json(
                ((new SelectListItem[] { new SelectListItem { Text = "", Value = "" } })
                .Union(from o in BlockModelService.GetModelParameterList(new Guid(modelID1))
                    join m in BlockModelService.GetModelParameterList(new Guid(modelID2))
                    on o.Text equals m.Text
                    select o)).OrderBy(f=>f.Text)
                    
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CompareModelResult(BlockModelCompareViewModel m)
        {
            m.ReportID = (uint)AllReports.ReportType.CompareModel;
            m.SelectedDomainsModel1Compact = m.SelectedDomainsModel1 != null ? string.Join(";", m.SelectedDomainsModel1.ToArray()) : null;
            m.SelectedDomainsModel2Compact = m.SelectedDomainsModel2 != null ? string.Join(";", m.SelectedDomainsModel2.ToArray()) : null;
            m.Model1Name = BlockModelService.GetModelAlias(m.Model1);
            m.Model2Name = BlockModelService.GetModelAlias(m.Model2);
            return View(m);
        }

        //public async Task<ActionResult> CompareModelResultPartial(BlockModelCompareViewModel m)
        //{
        //    return PartialView(await BlockModelService.CompareModelsAsync(m));
        //}


        [HttpPost]
        public ActionResult CompareModelResultPartial(BlockModelCompareViewModel m)
        {
            IReport r = BlockModelService.CompareModels(m);
            return PartialView(new BlockModelCompareViewModel { 
                Model1 = m.Model1,
                Model2 = m.Model2,
                DomainsModel1 = m.DomainsModel1,
                DomainsModel2 = m.DomainsModel2,
                GradeTonnageFieldID = m.GradeTonnageFieldID,
                GradeTonnageIncrement = m.GradeTonnageIncrement,
                ParametersIntersectionBothModels = m.ParametersIntersectionBothModels,
                ParametersModel1 = m.ParametersModel1,
                ParametersModel2 = m.ParametersModel2,
                ParametersView = r.ParametersView,
                Report = r.Report,
                ReportID = r.ReportID,
                ReportName = r.ReportName,
                SerializedChild = m.SerializedChild,
                SelectedDomainsModel1 = m.SelectedDomainsModel1,
                SelectedDomainsModel2 = m.SelectedDomainsModel2,
                SliceFieldID = m.SliceFieldID,
                SliceFilterFieldID = m.SliceFilterFieldID,
                SliceWidthX = m.SliceWidthX,
                SliceWidthY = m.SliceWidthY,
                SliceWidthZ = m.SliceWidthZ,
                FilterString = m.FilterString
            });
        }


        public ActionResult ReportViewerExportTo(BlockModelCompareViewModel m)
        {
            return ReportViewerExtension.ExportTo(AllReports.CreateModel(m).Report);
        }

        
        [HttpGet]
        public ActionResult UploadModel() { 
            // this is called when the view is cerated
            var model = new BlockModelUploadViewModel{
                Models = BlockModelService.GetModelList(),
                ProjectList = ProjectService.GetProjectList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadModelResult(BlockModelUploadViewModel m)
        {
            string ss = BlockModelService.GetBlockModelInfo(m.Model1);
            ss = ss + ".MMM";
            m.Test = ss;
            return View(m);
        }

        [HttpPost,  ValidateInput(true)]
        public ActionResult ImportModelProcessStart(BlockModelViewModel m) {
            // TODO do some processing here
            string bmFile = m.FileName;

            string formatSpecFile = m.FormatFileName;// "gf_compact_bm_format.xml";
            double xOrigin = m.XOrigin;
            double yOrigin = m.YOrigin;
            double zOrigin = m.ZOrigin; 
            string projID = m.Project;
            string alias = m.Alias;
            string notes = m.Notes;
            string stage = m.Stage;
            Guid gg = PrivateService.XODB_BM_STAGE;
            Task<string> val = BlockModelService.PerformBMImport(bmFile, formatSpecFile, projID, alias, UserService, this.getCurrentUserID(), notes, stage, gg);
            return View(m);
        }

        [HttpGet]
        public ActionResult EditModel()
        {
            var model = new BlockModelViewModel
            {
                FileNames = BlockModelService.GetFileNameList(),
                FormatFileNames = BlockModelService.GetFormatFileNameList(),
                Projects = ProjectService.GetProjectList()
            };
            model.Stages = ProjectService.GetStagesList(new Guid(model.Projects.First().Value));
            return View(model);
        }

        [HttpGet]
        public ActionResult ImportModel() {
            var model = new BlockModelViewModel
            {
                FileNames = BlockModelService.GetFileNameList(),
                FormatFileNames = BlockModelService.GetFormatFileNameList(),
                Projects = ProjectService.GetProjectList()
            };
            model.Stages = model.Projects.Any() ? ProjectService.GetStagesList(new Guid(model.Projects.First().Value)) : null;
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProject(string id = null, string verb = null)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageProjects, T("Couldn't edit the project.")))
                return new HttpUnauthorizedResult();
            //is new
            if (string.IsNullOrEmpty(id))
            {
                var model = new ProjectViewModel
                {
                    User = getCurrentUserID()
                };
                model.Contacts = UserService.GetContactList();
                model.Creator = model.User;
                return View(model);
            }
            //delete
            else if (verb == "delete")
            {
                ProjectService.DeleteProject(new Guid(id), getCurrentUserID());
                return RedirectToAction("ProjectList");
            }
            //edit
            else
            {
                var model = new ProjectViewModel
                {
                    Projects = ProjectService.GetProjectList(),
                    User = UserService.GetUserID(Services.WorkContext.CurrentUser.UserName)
                };
                model.Project = new Guid(id);
                model.Stages = ProjectService.GetStagesList(model.Project);
                model.Contacts = UserService.GetContactList();
                model.Creator = model.User;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditProject(ProjectViewModel m)
        {
            //Only process new projects atm
            if (m.Project != default(Guid))
                throw new NotImplementedException();

            //Do save project here
            if (ModelState.IsValid)
            {
                try
                {
                    if (!Services.Authorizer.Authorize(Permissions.ManageProjects, T("Couldn't create project.")))
                        return new HttpUnauthorizedResult();

                    //Validate
                    if (string.IsNullOrEmpty(m.ProjectName))
                        ModelState.AddModelError("ProjectName", T("Project name is required.").ToString());

                    if (ModelState.IsValid)
                    {
                        using (new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var now = DateTime.Now;
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
                        return RedirectToAction("ProjectList");
                    }

                    return View(m);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            if (m.Project == default(Guid))
                return EditProject();
            else
                return EditProject(m.Project.ToString());
        }

        public ActionResult GetStages(string projectID)
        {
            return Json(ProjectService.GetStagesList(new Guid(projectID))
                //.Select(x => new { Id = x.Value, Name = x.Text}) 
                , JsonRequestBehavior.AllowGet);
        }


        public ActionResult AppendToModel(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new NotImplementedException();
            var guid = new Guid(id);
            //SelectList fileList = BlockModelService.GetFileNameList();
            SelectList fileList = BlockModelService.GetUpdatedModelList();
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new Models.ModelsDataContext();
                var b = (d.BlockModels.OrderByDescending(x => x.Version).FirstOrDefault(x => x.BlockModelID == guid));
                var m = new BlockModelAppendViewModel
                {
                    BlockModelAlias = b.Alias,
                    Version = (b.Version + 1),
                    BlockModelID = guid,
                    FileNames = fileList
                };

                return View(m);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AppendToModelProcessStart(BlockModelAppendViewModel m)
        {

            // get the currently mapped model columns
            List<string> columnNames = BlockModelService.GetImportFileColumnsAsList(m.BlockModelID, m.FileName, m.BlockModelAlias);
            SelectList sl = new SelectList(columnNames);
            // get the next available column in the database to write update to, requires a search in the meta data for column names
            m.FileColumnNames = sl;
            return View(m);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AppendToModelProcessGo(BlockModelAppendViewModel m)
        {
            // Call into the block model library to append the given column using the column map provided.
            // this will attempt to match the BlockModelID of target model, and XC, YC and ZC  coordinates of every record and insert the value
            List<string> columnNames = BlockModelService.GetImportFileColumnsAsList(m.BlockModelID, m.FileName, m.BlockModelAlias);            
            string columnToAdd = m.ColumnName;
            int columnIndexToAdd = 3;
            BlockModelService.PerformBMImportAppend(m.BlockModelID, m.FileName, m.BlockModelAlias, columnToAdd, columnIndexToAdd);
            return View(m);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AppendToModelUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucCallbacks", UIHelper.AppendModelValidationSettings, UIHelper.ucCallbacks_AppendModelUploadComplete);
            return null;
        }

        public ActionResult ModelsToAuthoriseList()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ModelsToAuthoriseListPartial()
        {
            return PartialView("ModelsToAuthoriseListPartial");
        }

        public ActionResult ModelList()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ModelListPartial()
        {
            return PartialView("ModelListPartial");
        }


        public ActionResult ProjectList()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ProjectListPartial()
        {
            return PartialView("ProjectListPartial");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProjectListNewPartial(Project o)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!Services.Authorizer.Authorize(Permissions.ManageProjects, T("Couldn't create project.")))
                        return new HttpUnauthorizedResult();

                    //Validate
                    if (string.IsNullOrEmpty(o.ProjectName))
                        ModelState.AddModelError("ProjectName", T("Project name is required.").ToString());

                    if (ModelState.IsValid)
                    {
                        using (new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var p = new ProjectsDataContext();
                            o.ProjectName = (o.ProjectName == "null") ? null : o.ProjectName.Substring(1, o.ProjectName.Length - 2);
                            o.Comment = (o.Comment == "null") ? null : o.Comment.Substring(1, o.Comment.Length - 2);
                            o.ProjectID = Guid.NewGuid();
                            p.Projects.InsertOnSubmit(o);
                            p.SubmitChanges();

                            //projects.SaveChanges();
                        }
                        return PartialView("ProjectListPartial");
                    }

                    return View(o);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("ProjectListPartial");
        }

        public ActionResult ModelParameters()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ModelParametersPartial()
        {
            return PartialView("ModelParametersPartial");
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult ModelParametersEdit(string id)
        {
            var model = ParameterService.GetParameter(new Guid(id));
            model.Units = ParameterService.GetUnitsList();
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ModelParametersEdit(BlockModelParameterViewModel m)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (!Services.Authorizer.Authorize(Permissions.ManageProjects, T("Couldn't update parameter.")))
                        return new HttpUnauthorizedResult();
                    
                   
                    //Validate
                    if (!m.UnitID.HasValue)
                        ModelState.AddModelError("UnitID", T("Unit is required.").ToString());

                    if (ModelState.IsValid)
                    {
                        using (new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var d = new ModelsDataContext();
                            var x = from p in d.Parameters where p.ParameterID==m.ParameterID select p;
                            var o = x.First();
                            o.UnitID = m.UnitID;
                            d.SubmitChanges();                            
                        }
                        return RedirectToAction("ModelParameters");
                    }

                    return View(m);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return View(m);
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult AuthoriseModel(string id)
        {
            var model = ParameterService.GetApproval(new Guid(id));
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AuthoriseModel(BlockModelApproveViewModel m, string submit)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (!Services.Authorizer.Authorize(Permissions.ManageProjects, T("Couldn't update model.")))
                        return new HttpUnauthorizedResult();

                    if (submit == "Approve")
                    {

                        if (ModelState.IsValid)
                        {
                            using (new TransactionScope(TransactionScopeOption.Suppress))
                            {
                                var note = string.Format("Model [Name: {0} ID: ({1})] was approved by ({2}).", m.BlockModelAlias, m.BlockModelID, Services.WorkContext.CurrentUser.UserName);
                                Logger.Information(note);
                                using (new TransactionScope(TransactionScopeOption.Suppress))
                                {
                                    var d = new ModelsDataContext();
                                    var o = d.BlockModels.Where(f => f.BlockModelID == m.BlockModelID).Single();
                                    o.ApproverContactID = getCurrentUserID();
                                    if (o.AuthorContactID.HasValue)
                                        UserService.EmailUsers(new Guid[] { o.AuthorContactID.Value } , "Model approved", note);
                                    var n = new BlockModelMetadata();
                                    n.BlockModelMetadataID = Guid.NewGuid();
                                    n.BlockModelID = m.BlockModelID.Value;
                                    n.ParameterID = PrivateService.XODB_GUID_LOG;
                                    var oc = new Occurrence();
                                    oc.ID = Guid.NewGuid();
                                    oc.ContactID = o.ApproverContactID.Value;
                                    oc.Occurred = DateTime.Now;
                                    oc.Status = (uint)Occurrence.StatusCode.OK;
                                    n.BlockModelMetadataText = oc.ToJson();
                                    d.BlockModelMetadatas.InsertOnSubmit(n);
                                    d.SubmitChanges();
                                }
                            }
                        }
                    }
                    else if (submit == "Notify")
                    {
                        var error = string.Format("Model [Name: {0} ID: ({1})] was not approved by ({2}).", m.BlockModelAlias, m.BlockModelID, Services.WorkContext.CurrentUser.UserName);
                        Logger.Information(error);
                        using (new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var d = new ModelsDataContext();
                            var o = d.BlockModels.Where(f => f.BlockModelID == m.BlockModelID && f.AuthorContactID != null).Select(f => (Guid)f.AuthorContactID).ToArray();
                            UserService.EmailUsers(o, "Model not accepted", error);
                            var n = new BlockModelMetadata();
                            n.BlockModelMetadataID = Guid.NewGuid();
                            n.BlockModelID = m.BlockModelID.Value;
                            n.ParameterID = PrivateService.XODB_GUID_LOG;
                            var oc = new Occurrence();
                            oc.ID = Guid.NewGuid();
                            oc.ContactID = getCurrentUserID();
                            oc.Occurred = DateTime.Now;
                            oc.Status = (uint)Occurrence.StatusCode.Notified;
                            n.BlockModelMetadataText = oc.ToJson();
                            d.BlockModelMetadatas.InsertOnSubmit(n);
                            d.SubmitChanges();
                        }

                    }
                    return RedirectToAction("ModelsToAuthoriseList");
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return RedirectToAction("AuthoriseModel", new { id = m.BlockModelID });
            
        }

        public ActionResult ModelParametersDelete(string  id)
        {            
            //TODO: Delete parameter
            return RedirectToAction("ModelParameters");
        }

        

        public ActionResult ProjectInfo()
        {
            dynamic packageDisplay = Services.New.ProjectInfo(
                ProjectCount: 10
                );
            return new ShapeResult(this, packageDisplay);
        }

        public ActionResult Index()
        {
            return View();            
        }

       
    }
}
