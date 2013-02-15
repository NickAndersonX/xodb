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
using Orchard.Media.Models;
using Orchard.Media.Services;
using System.Transactions;

using XODBImportLib;
using XODB.ViewModels;
using System.Threading.Tasks;
using XODB.Reports;
using XODBImportLib.FormatSpecification;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using XODB.Helpers;

namespace XODB.Services {

    [UsedImplicitly]
    public class BlockModelService : IBlockModelService {
        private readonly IStorageProvider _storageProvider;
        private readonly IOrchardServices _orchardServices;
        private readonly IMediaService _mediaServices;
        private readonly IPrivateDataService _privateServices;

        public BlockModelService(IStorageProvider storageProvider, IOrchardServices orchardServices, IMediaService mediaServices, IPrivateDataService privateService) {
            _storageProvider = storageProvider;
            _orchardServices = orchardServices;
            _mediaServices = mediaServices;
            _privateServices = privateService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<MediaFile> GetNewBlockModelFiles()
        {
            return _mediaServices.GetMediaFiles(@"XODB\BlockModels"); 
        }
        public IEnumerable<MediaFile> GetUpdateFileNameList()
        {
            return _mediaServices.GetMediaFiles(@"XODB\ModelUpdates"); 
        }
        
        public IEnumerable<MediaFile> GetNewFormatFiles()
        {
            return _mediaServices.GetMediaFiles(@"XODB\Definitions");
        }


        public async Task<string> PerformBMImportAppend(Guid guid, string bmFileName, string alias, string columnNameToImport, int columnIndexToImport)
        {
            return await Task<string>.Run(() => ImportBMFromFileAppend(guid, bmFileName, alias, columnNameToImport, columnIndexToImport)).ConfigureAwait(false);
        }

        public List<string> GetImportFileColumnsAsList(Guid guid, string bmFileName, string modelAlias)
        {

            List<string> columnNames = new List<string>();
            string scs = global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString;
            string connString = scs;
            BaseImportTools bit = new BaseImportTools();

            string targetFolder;
            bool attmemptModelLoad;
            string originalName = bmFileName;
            bmFileName = ExtractBMFromZip(bmFileName, out targetFolder, out attmemptModelLoad);
            IStorageFile bmFile = _storageProvider.GetFile(bmFileName);
            Stream bmFileStream = bmFile.OpenRead();
            XODBImportLib.FormatSpecification.ImportDataMap idm = null;
            double _originX = -1;
            double _originY = -1;
            double _originZ = -1;

            try
            {
                StreamReader sr = new StreamReader(bmFileStream);
                string headerLine = "";
                string firstDataLine = "";
                if (sr != null)
                {
                    headerLine = sr.ReadLine();
                    firstDataLine = sr.ReadLine();

                    bit.ParseDataLinesForOrigins(headerLine, firstDataLine, out _originX, out _originY, out _originZ);
                    // auto generate a format defintion based on Goldfields typical input column data
                    idm = bit.AutoGenerateFormatDefinition(headerLine);
                }
                sr.Close();

            }
            catch (Exception ex)
            {

            }

            if (idm != null)
            {


                foreach (ColumnMap cm in idm.columnMap)
                {
                    columnNames.Add(cm.sourceColumnName);
                }
            }
            return columnNames;
        }
       

        private string ImportBMFromFileAppend(Guid guid, string bmFileName, string alias, string columnNameToImport, int columnIndexToImport)
        {

            string statusMessage = "";
            string scs = global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString;
            string connString = scs;
            BaseImportTools bit = new BaseImportTools();
            string targetFolder;
            bool attmemptModelLoad;
            string originalName = bmFileName;
            bmFileName = ExtractBMFromZip(bmFileName, out targetFolder, out attmemptModelLoad);
            IStorageFile bmFile = _storageProvider.GetFile(bmFileName);
            Stream bmFileStream = bmFile.OpenRead();
            try
            {
                bool skip = true;
                if (!skip)
                {
                    // Special append method for Goldfields requirements - contains X, Y, Z, and value to append.
                    // target model must have matching X, Y and Z centroids.
                    // auto generate a format defintion based on Goldfields typical input column data                  
                    bit.PerformBMAppend(bmFileStream, guid, alias, columnNameToImport, columnIndexToImport, scs);
                }

            }
            catch (Exception ex)
            {
                statusMessage += "Error appending data to block model:\n" + ex.ToString();
            }
            finally {
                bmFileStream.Close();
                _storageProvider.DeleteFile(bmFileName);
                _storageProvider.DeleteFolder(targetFolder);
            }

            return "";

        }

        public async Task<string> PerformBMImport(string bmFileName, string formatFileName, string projectID, string alias, IUsersService UserService, Guid currentUser, string notes, string stage, Guid stageMetaID)
        {

            return await Task<string>.Run(() => ImportBMFromFile(bmFileName, formatFileName, projectID, alias, UserService, currentUser, notes, stage, stageMetaID)).ConfigureAwait(false);
        }

        public string ImportBMFromFile(string bmFileName, string formatFileName, string projectID, string alias, IUsersService UserService, Guid authorGuid, string notes, string stage, Guid stageMetaID)
        {
            
            List<string> domains = null;

            string targetFolder;
            bool attmemptModelLoad;
            string originalName = bmFileName;
            bmFileName = ExtractBMFromZip(bmFileName, out targetFolder, out attmemptModelLoad);

            ModelImportStatus mos = DoNewModelImport(bmFileName, formatFileName, projectID, alias, authorGuid, ref domains, targetFolder, attmemptModelLoad, notes, stage, stageMetaID);
            mos.importTextFileName = bmFileName +"(from "+originalName+")";
            mos.targetModelName = alias;
            Guid userGuid = UserService.GetUserID("XSTRACT\\nanderson");
            List<Guid> recipients = new List<Guid>();
            recipients.Add(authorGuid);
            recipients.Add(userGuid);
            
            string msg = mos.GenerateStringMessage();
            string subjectLine = "Model import complete for " + mos.importTextFileName;
            if (mos.finalErrorCode > 0) {
                subjectLine = "Model load failure for " + mos.importTextFileName;
            }

            UserService.EmailUsers(recipients.ToArray(), subjectLine, msg);

            return msg;
        }

        /// <summary>
        /// Import the block model.  This is ultimatly carried out throught he import library which must be 
        /// passed the streams for the import file (and defintiion possibly).
        /// </summary>
        /// <param name="bmFileName"></param>
        /// <param name="formatFileName"></param>
        /// <param name="projectID"></param>
        /// <param name="alias"></param>
        /// <param name="authorGuid"></param>
        /// <param name="res2"></param>
        /// <param name="domains"></param>
        /// <param name="targetFolder"></param>
        /// <param name="attmemptModelLoad"></param>
        /// <param name="notes"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        private ModelImportStatus  DoNewModelImport(string bmFileName, string formatFileName, string projectID, string alias, Guid authorGuid, ref List<string> domains, string targetFolder, bool attmemptModelLoad, string notes, string stage, Guid stageMetaID)
        {
            ModelImportStatus mos = new ModelImportStatus();
            mos.finalErrorCode = ModelImportStatus.OK;
            double _originX = -1;
            double _originY = -1;
            double _originZ = -1;

            string scs = global::System.Configuration.ConfigurationManager.ConnectionStrings["XODBConnectionString"].ConnectionString;
            string connString = scs;
            BaseImportTools bit = new BaseImportTools();
            if (attmemptModelLoad)
            {
                //IStorageFile formatFile = _storageProvider.GetFile(formatFileName);
                IStorageFile bmFile = _storageProvider.GetFile(bmFileName);
                Stream bmFileStream = bmFile.OpenRead();
                Stream bmTempFileStream = bmFile.OpenRead();
                // try and automatically detect the origin coordinates from the file itself - a datamine file 
                // always carries the origin coords in XMORIG, YMORIG, ZMORIG
                
                XODBImportLib.FormatSpecification.ImportDataMap idm = null;
                try
                {                   
                    StreamReader sr = new StreamReader(bmTempFileStream);
                    string headerLine = "";
                    string firstDataLine = "";
                    if (sr != null)
                    {
                        headerLine = sr.ReadLine();
                        firstDataLine = sr.ReadLine();
                        bit.ParseDataLinesForOrigins(headerLine, firstDataLine, out _originX, out _originY, out _originZ);
                        // auto generate a format defintion based on Goldfields typical input column data
                        idm = bit.AutoGenerateFormatDefinition(headerLine);
                    }
                    sr.Close();

                }
                catch (Exception ex) {
                    mos.AddWarningMessage("Unable to auto detect origin and other format information from the file");
                }
                //Stream ffFileStream = formatFile.OpenRead();
                bool skipLoad = false;  // for debugging purposes to skip the actual laod
                if (!skipLoad)
                {
                    using (new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        try
                        {                            
                            Guid blockModelGUID = Guid.NewGuid();
                            mos.modelID = blockModelGUID;
                            domains = bit.PerformBMImport(mos, blockModelGUID, bmFileStream, null, idm, _originX, _originY, _originZ, null, 1000, projectID, alias, authorGuid, connString);
                            List<Tuple<string, string>> doms = new List<Tuple<string, string>>();
                            string domainColumnName = "Domain";
                            foreach (string ss in domains) {
                                doms.Add(new Tuple<string, string>(domainColumnName, ss));
                            }
                            XODB.Helpers.BMImportHelper.UpdateDomains(doms, blockModelGUID);
                            XODB.Helpers.BMImportHelper.AddModelNotes(notes, blockModelGUID);
                            XODB.Helpers.BMImportHelper.UpdateStage(blockModelGUID, stageMetaID, stage);
                        }
                        catch (Exception ex)
                        {
                            mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
                            mos.AddErrorMessage("Error importing block model:\n"+ex.ToString());
                        }



                    }
                }
                //TODO call into import library with the stream object for the import

                bmFileStream.Close();
                _storageProvider.DeleteFile(bmFileName);
                _storageProvider.DeleteFolder(targetFolder);
            }
            return mos;
        }

        /// <summary>
        /// Extract the block model file from the nominated Zip file.  A temp folder is created to store the file
        /// and the path to that file is generated for use when importing.
        /// </summary>
        /// <param name="bmFileName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="attmemptModelLoad"></param>
        /// <returns></returns>
        private string ExtractBMFromZip(string bmFileName, out string targetFolder, out bool attmemptModelLoad)
        {
            IStorageFile bmZipFile = _storageProvider.GetFile(bmFileName);
            targetFolder = bmFileName + "extracted";
            List<string> filesInZip = new List<string>();
            attmemptModelLoad = false;
            if (bmFileName.EndsWith(".zip"))
            {
                Stream bmZipFileStream = bmZipFile.OpenRead();
                // extract file to a temp location
                using (var fileInflater = ZipFile.Read(bmZipFileStream))
                {
                    foreach (ZipEntry entry in fileInflater)
                    {
                        if (entry == null) { continue; }

                        if (!entry.IsDirectory && !string.IsNullOrEmpty(entry.FileName))
                        {

                            // skip disallowed files
                            //if (FileAllowed(entry.FileName, false))
                            // {
                            string fullFileName = _storageProvider.Combine(targetFolder, entry.FileName);
                            filesInZip.Add(fullFileName);
                            using (var stream = entry.OpenReader())
                            {
                                // the call will return false if the file already exists
                                if (!_storageProvider.TrySaveStream(fullFileName, stream))
                                {

                                    // try to delete the file and save again
                                    try
                                    {
                                        _storageProvider.DeleteFile(fullFileName);
                                        _storageProvider.TrySaveStream(fullFileName, stream);
                                    }
                                    catch (ArgumentException)
                                    {
                                        // ignore the exception as the file doesn't exist
                                    }
                                }
                            }
                            //}
                        }
                    }
                }
                bmZipFileStream.Close();
                if (filesInZip.Count() > 0)
                {
                    bmFileName = filesInZip.First();
                    attmemptModelLoad = true;
                }
            }
            return bmFileName;
        }

        

       
        public IEnumerable<BlockModel> GetModels()
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ModelsDataContext();
                return d.BlockModels.ToArray();
            }
        }


        public string GetBlockModelInfo(Guid bm) {
            string res = "";
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                try
                {

                    BaseImportTools bit = new BaseImportTools();
                    string ss1 = bit.TestConnection();
                    var d = new ModelsDataContext();
                    List<BlockModel> bl = d.BlockModels.ToList();
                    foreach (BlockModel aModel in bl)
                    {
                        res += aModel.Alias;
                    }
                }
                catch (Exception ex) {
                    res += "Error " + ex.ToString();
                }
            }
            return res;
        }

        public string GetModelAlias(Guid modelID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ModelsDataContext();
                return (from o in d.BlockModels where o.BlockModelID == modelID select o.Alias).FirstOrDefault();
            }
        }

        public IEnumerable<Tuple<Parameter, BlockModelMetadata>> GetModelParameters(Guid modelID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ModelsDataContext();
                var o = from m in d.BlockModelMetadatas where m.BlockModelID == modelID
                        join p in d.Parameters on m.ParameterID equals p.ParameterID
                        select new { m, p }
                        ; 
                return o.OrderBy(f=>f.p.DefaultParameterText).ToArray().Select(f=>new Tuple<Parameter,BlockModelMetadata>(f.p, f.m)); 
            }
        }

        public IEnumerable<Tuple<string, string>> GetModelDomains(Guid modelID)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                //Slow Method
                //var d = new ModelsDataContext();
                //return d.GetUniqueDomains(modelID).Select(x => new Tuple<string,string>("Domain", x.Domain)).OrderBy(x => x).ToList();
                
                //Fast Method
                return _privateServices.GetFirstMetadata<List<Tuple<string, string>>>(modelID, f => f.MetaDataType == "Domains");
            }
        }

        public void CheckModels()
        {
            //TODO:
            // Removes old files, removes old models, checks for new files (future - automatically add model?)
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ModelsDataContext();
                d.DropOldModels();
            }
        }

        public async Task<IReport> CompareModelsAsync(BlockModelCompareViewModel models)
        {
            return await Task<IReport>.Run(() => CompareModels(models));
        }

        public IReport CompareModels(BlockModelCompareViewModel models)
        {
            return AllReports.CreateModel(models);
        }

        //Static Methods
        public static DataSet CompareModelsResult(BlockModelCompareViewModel m)
        {
            DataSet ds = new DataSet("CompareModelsResult");

            var y = m.FilterString.Deserialize<dynamic>();
            var filter = new List<Tuple<string, string, string, string, string>>();
            foreach (object[] k in y)
            {
                filter.Add(new Tuple<string, string, string, string, string>(
                    k[0] as string, //1 Model# (1 or 2)
                    k[1] as string, //2 Boolean Comparator (AND, OR)
                    k[2] as string, //3 Guid (FieldID)
                    k[3] as string, //4 Math Comparator (>=,<=,=)
                    k[4] as string  //5 Value (0.05)
                    ));
            }
            string filter1 = string.Join(";", (from o in filter
                             where o.Item1 == "1"
                             select
                                 o.Item2.CleanTokenForSQL() + "," +
                                 o.Item3.CleanTokenForSQL() + "," +
                                 o.Item4.CleanTokenForSQL() + "," +
                                 o.Item5.CleanTokenForSQL()).ToArray());
            filter1 = string.IsNullOrEmpty(filter1) ? filter1 : filter1 + ";";
            string filter2 = string.Join(";", (from o in filter
                            where o.Item1 == "2"
                            select
                                o.Item2.CleanTokenForSQL() + "," +
                                o.Item3.CleanTokenForSQL() + "," +
                                o.Item4.CleanTokenForSQL() + "," +
                                o.Item5.CleanTokenForSQL()).ToArray());
            filter2 = string.IsNullOrEmpty(filter2) ? filter2 : filter2 + ";";

            string domains1 = m.SelectedDomainsModel1Compact != null ? m.SelectedDomainsModel1Compact.CleanTokenForSQL() + ";" : "";
            string domains2 = m.SelectedDomainsModel2Compact !=null ? m.SelectedDomainsModel2Compact.CleanTokenForSQL() + ";": "";
            
            //Test DataSet
            var p = new DataTable("Test1Text");
            var x = new string[] { "asdasd", "asdasd", "asss", "asddasd", "reerrr" };
            var s = from i in x select new { Test = i };
            p.Columns.Add("Test");
            s.CopyToDataTable(p);
            ds.Tables.Add(p);

            //Test Dataset
            double[][] z = new double[][] { new[] { 0.5, 0.7 }, new[] { 40.5, 60.7 } };
            var t = from i in z select new { Argument = i[0], Value = i[1] };
            var g = new DataTable("Test2Tuple");
            g.Columns.Add("Argument", typeof(double));
            g.Columns.Add("Value", typeof(double));
            t.CopyToDataTable(g);
            ds.Tables.Add(g);

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var d = new ModelsDataContext();

                //////Get Curves
                var cmd = d.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[X_SP_GradeTonnage]";
                cmd.CommandType = CommandType.StoredProcedure;

                var parm1 = cmd.CreateParameter();
                parm1.ParameterName = "@gt_guid";
                parm1.DbType = DbType.Guid;
                parm1.Value = m.GradeTonnageFieldID;
                cmd.Parameters.Add(parm1);

                var parm2 = cmd.CreateParameter();
                parm2.ParameterName = "@bm1_guid";
                parm2.DbType = DbType.Guid;
                parm2.Value = m.Model1;
                cmd.Parameters.Add(parm2);

                var parm3 = cmd.CreateParameter();
                parm3.ParameterName = "@bm2_guid";
                parm3.DbType = DbType.Guid;
                parm3.Value = m.Model2;
                cmd.Parameters.Add(parm3);

                var parm4 = cmd.CreateParameter();
                parm4.ParameterName = "@gt_increment";
                parm4.DbType = DbType.Decimal;
                parm4.Value = m.GradeTonnageIncrement;
                cmd.Parameters.Add(parm4);

                var parm5 = cmd.CreateParameter();
                parm5.ParameterName = "@filter1";
                parm5.DbType = DbType.String;
                parm5.Value = filter1;
                cmd.Parameters.Add(parm5);

                var parm6 = cmd.CreateParameter();
                parm6.ParameterName = "@filter2";
                parm6.DbType = DbType.String;
                parm6.Value = filter2;
                cmd.Parameters.Add(parm6);

                var parm7 = cmd.CreateParameter();
                parm7.ParameterName = "@domains1";
                parm7.DbType = DbType.String;
                parm7.Value = domains1;
                cmd.Parameters.Add(parm7);

                var parm8 = cmd.CreateParameter();
                parm8.ParameterName = "@domains2";
                parm8.DbType = DbType.String;
                parm8.Value = domains2;
                cmd.Parameters.Add(parm8);

                var output = cmd.CreateParameter();
                output.ParameterName = "@filterString";
                output.DbType = DbType.String;
                output.Size = 4000;
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                /////Get Table

                var cmdc = d.Connection.CreateCommand();
                cmdc.CommandText = "[dbo].[X_SP_GradeTonnageComparison]";
                cmdc.CommandType = CommandType.StoredProcedure;

                var parmc1 = cmd.CreateParameter();
                parmc1.ParameterName = "@gt_guid";
                parmc1.DbType = DbType.Guid;
                parmc1.Value = m.GradeTonnageFieldID;
                cmdc.Parameters.Add(parmc1);

                var parmc2 = cmd.CreateParameter();
                parmc2.ParameterName = "@bm1_guid";
                parmc2.DbType = DbType.Guid;
                parmc2.Value = m.Model1;
                cmdc.Parameters.Add(parmc2);

                var parmc3 = cmd.CreateParameter();
                parmc3.ParameterName = "@bm2_guid";
                parmc3.DbType = DbType.Guid;
                parmc3.Value = m.Model2;
                cmdc.Parameters.Add(parmc3);

                var parmc5 = cmd.CreateParameter();
                parmc5.ParameterName = "@filter1";
                parmc5.DbType = DbType.String;
                parmc5.Value = filter1;
                cmdc.Parameters.Add(parmc5);

                var parmc6 = cmd.CreateParameter();
                parmc6.ParameterName = "@filter2";
                parmc6.DbType = DbType.String;
                parmc6.Value = filter2;
                cmdc.Parameters.Add(parmc6);

                var parmc7 = cmd.CreateParameter();
                parmc7.ParameterName = "@domains1";
                parmc7.DbType = DbType.String;
                parmc7.Value = domains1;
                cmdc.Parameters.Add(parmc7);

                var parmc8 = cmd.CreateParameter();
                parmc8.ParameterName = "@domains2";
                parmc8.DbType = DbType.String;
                parmc8.Value = domains2;
                cmdc.Parameters.Add(parmc8);

                var outputc = cmd.CreateParameter();
                outputc.ParameterName = "@filterString";
                outputc.DbType = DbType.String;
                outputc.Size = 4000;
                outputc.Direction = ParameterDirection.Output;
                cmdc.Parameters.Add(outputc);

                try
                {
                    var gt = new DataTable("gt");
                    gt.Columns.Add("m", typeof(string));
                    gt.Columns.Add("grade", typeof(decimal));
                    gt.Columns.Add("tonnage", typeof(decimal));

                    var gft = new DataTable("gfs");
                    gft.Columns.Add("FilterString", typeof(string));

                    var gfc = new DataTable("gfc");
                    gfc.Columns.Add("Resource Category", typeof(string));
                    gfc.Columns.Add("Model 1 Tonnes", typeof(decimal));
                    gfc.Columns.Add("Model 1 Grade", typeof(decimal));
                    gfc.Columns.Add("Model 2 Tonnes", typeof(decimal));
                    gfc.Columns.Add("Model 2 Grade", typeof(decimal));
                    gfc.Columns.Add("Absolute Difference", typeof(decimal));


                    //Let's actually run the queries
                    d.Connection.Open();

                    if (m.GradeTonnageIncrement != 0)
                    {
                        var reader = cmd.ExecuteReader();
                        gt.Load(reader, LoadOption.OverwriteChanges);

                        //reader.NextResult(); // Only 1 Resultset
                        //var gt2 = new DataTable("gt2");
                        //gt2.Load(reader, LoadOption.OverwriteChanges);
                        //ds.Tables.Add(gt2);
                    }

                    var readerc = cmdc.ExecuteReader();
                    gfc.Load(readerc, LoadOption.OverwriteChanges);
                    
                    gft.Rows.Add(outputc.Value as string); //filterString                    

                    ds.Tables.Add(gt);
                    ds.Tables.Add(gft);
                    ds.Tables.Add(gfc);
                }
                finally
                {
                    d.Connection.Close();
                }
                return ds;
            }
        }
        
    }
}
