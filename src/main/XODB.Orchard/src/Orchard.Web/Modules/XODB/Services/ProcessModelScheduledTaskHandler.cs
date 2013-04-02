using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.Tasks.Scheduling;
using Orchard.Logging;
using Orchard.ContentManagement;
using XODB.Models;

namespace XODB.Services
{
    public class ProcessModelScheduledTaskHandler : IScheduledTaskHandler
    {
        public const string TASK_TYPE_PROCESS_MODEL = "ProcessModelScheduled";
        private readonly IBlockModelService _blockModelService;

        public ILogger Logger { get; set; }

        public ProcessModelScheduledTaskHandler(IBlockModelService blockModelService)
        {
            _blockModelService = blockModelService;           
            Logger = NullLogger.Instance;
        }

        public void Process(ScheduledTaskContext context)
        {
            if (context.Task.TaskType == TASK_TYPE_PROCESS_MODEL && context.Task.ContentItem != null)
            {
                try
                {
                    var m = context.Task.ContentItem.As<BlockModelPart>();
                    _blockModelService.ProcessModel(m.BmFileName, m.FormatFileName, m.ProjectName, m.Alias, m.UserID, m.Notes, m.Stage, m.StageMetaID, m.Emails);
                }
                catch (Exception e)
                {
                    this.Logger.Error(e, e.Message);
                }
                finally
                {

                }
            }
        }

    }
}