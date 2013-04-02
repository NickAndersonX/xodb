using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.Settings;
using Orchard.Tasks.Scheduling;
using Orchard.ContentManagement;

namespace XODB.Helpers
{
    public static class TaskHelper
    {
     
        public static void EmailAsync(this IScheduledTaskManager _taskManager, ContentItem contentItem)
        {
            var tasks = _taskManager.GetTasks(Services.EmailScheduledTaskHandler.TASK_TYPE_EMAIL);
            if (tasks == null || tasks.Count() < 100)
                _taskManager.CreateTask(Services.EmailScheduledTaskHandler.TASK_TYPE_EMAIL, DateTime.UtcNow, contentItem);
        }

        public static void ProcessModelAsync(this IScheduledTaskManager _taskManager, ContentItem contentItem)
        {
            var tasks = _taskManager.GetTasks(Services.ProcessModelScheduledTaskHandler.TASK_TYPE_PROCESS_MODEL);
            if (tasks == null || tasks.Count() < 100)
                _taskManager.CreateTask(Services.ProcessModelScheduledTaskHandler.TASK_TYPE_PROCESS_MODEL, DateTime.UtcNow, contentItem);
        }

        public static void AppendModelAsync(this IScheduledTaskManager _taskManager, ContentItem contentItem)
        {
            var tasks = _taskManager.GetTasks(Services.AppendModelScheduledTaskHandler.TASK_TYPE_APPEND_MODEL);
            if (tasks == null || tasks.Count() < 100)
                _taskManager.CreateTask(Services.AppendModelScheduledTaskHandler.TASK_TYPE_APPEND_MODEL, DateTime.UtcNow, contentItem);
        }

        public class TaskContext
        {
            public string CurrentCulture { get; set; }
            public ISite CurrentSite { get; set; }

            public TaskContext(WorkContext workContext)
            {
                CurrentCulture = workContext.CurrentCulture;
                CurrentSite = workContext.CurrentSite;
            }

            public WorkContext Transcribe(WorkContext workContext)
            {
                workContext.CurrentCulture = CurrentCulture;
                workContext.CurrentSite = CurrentSite;

                return workContext;
            }
        }
    }
}