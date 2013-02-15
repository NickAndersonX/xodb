using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;

namespace XODB.Services
{
    [ServiceContract]
    public interface IProjectsService : IDependency {

        [OperationContract]
        IEnumerable<Project> GetProjects();

        [OperationContract]
        IEnumerable<ProjectPlanTask> GetStages(Guid projectID);

        [OperationContract]
        void DeleteProject(Guid projectID, Guid contactID);

        [OperationContract]
        void EmailAllProjectOwners(string subject, string body);

    }
}