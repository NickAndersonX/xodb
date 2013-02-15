using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Orchard;
using XODB.Models;
using System.ServiceModel;
using Orchard.Media.Models;
using XODB.ViewModels;
using System.Threading.Tasks;

namespace XODB.Services
{
     [ServiceContract]
    public interface IBlockModelService : IDependency 
    {
         [OperationContract]
         IEnumerable<MediaFile> GetNewBlockModelFiles();

         [OperationContract]
         IEnumerable<MediaFile> GetUpdateFileNameList();

         [OperationContract]
         IEnumerable<MediaFile> GetNewFormatFiles();


         [OperationContract]
         IEnumerable<BlockModel> GetModels();

         [OperationContract]
         string GetModelAlias(Guid modelID);

         [OperationContract]
         IEnumerable<Tuple<Parameter, BlockModelMetadata>> GetModelParameters(Guid modelID);

         
         [OperationContract]
         string GetBlockModelInfo(Guid guid);

         [OperationContract]
         Task<string> PerformBMImport(string bmFileName, string formatFileName, string projectID, string alias, IUsersService UserService, Guid userID, string notes, string stage, Guid stageMetaID);

         [OperationContract]
         Task<string> PerformBMImportAppend(Guid guid, string modelFileName, string alias, string columnNameToAdd, int columnIndexToAdd);

       

         [OperationContract]
         List<string> GetImportFileColumnsAsList(Guid guid, string modelFileName, string modelAlias);

		 [OperationContract]
         IEnumerable<Tuple<string,string>> GetModelDomains(Guid modelID);

         /// <summary>
         /// Removes old files, removes old models, checks for new files (future - automatically add model?)
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         void CheckModels();

         [OperationContract]
         Task<IReport> CompareModelsAsync(BlockModelCompareViewModel models);

         [OperationContract]
         IReport CompareModels(BlockModelCompareViewModel models);

    }
}