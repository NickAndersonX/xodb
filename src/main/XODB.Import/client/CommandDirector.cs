using XODB.Import.Client.IO;
using XODB.Import.Client.Processing;
using XODB.Import.Client.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Import;
using XODB.Import.DataWrappers;
using XODB.Import.FormatSpecification;
using XODB.Import.Client.Processing;
using Xstract.Import.LAS;

namespace XODB.Import.Client
{
    public class CommandDirector
    {

        private BackgroundWorker backgroundWorker = null;

        private static string connectionString = "Data Source=XODBDB;Initial Catalog=XODB;Integrated Security=True";
        //TODO: Override in interface if we need to support > 1 DB
        public static string ConnectionString { get { return connectionString; } set { connectionString = value; } }

        public CommandDirector() { }


        /// <summary>
        /// Carry out the block model import
        /// </summary>
        /// <param name="SelectedBMFile"></param>
        /// <param name="SelectedFormatBMFile"></param>
        /// <param name="importMap"></param>
        /// <param name="blockRawFileReader"></param>
        /// <returns></returns>
        internal bool DoBMImport(string SelectedBMFile, string SelectedFormatBMFile, ImportDataMap importMap, RawFileReader blockRawFileReader, string XODBProjectID, string modelAlias)
        {
            BaseImportTools bit = new BaseImportTools();
            int cxColumnID = importMap.GetColumnIDMappedTo("CentroidX");
            int cyColumnID = importMap.GetColumnIDMappedTo("CentroidY");
            int czColumnID = importMap.GetColumnIDMappedTo("CentroidZ");
            
            ColumnStats xOrigin = blockRawFileReader.GetDimensions(cxColumnID);
            ColumnStats yOrigin = blockRawFileReader.GetDimensions(cyColumnID);
            ColumnStats zOrigin = blockRawFileReader.GetDimensions(czColumnID);

            int approxNumLines = xOrigin.count;


            Stream bmFileStream = new FileStream(SelectedBMFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
           // Stream bmFileStream = new FileStream(SelectedBMFile, FileMode.Open);
            ModelImportStatus mos = new ModelImportStatus();
            Guid newModelGuid = Guid.NewGuid();
            
            Guid authorGuid = new Guid();
            List<string> status = bit.PerformBMImport(mos, newModelGuid, bmFileStream, null, importMap, xOrigin.min, yOrigin.min, zOrigin.min, backgroundWorker, approxNumLines, XODBProjectID, modelAlias, authorGuid, ConnectionString);
            return true;
        }

        internal void SetCurrentWorkerThread(System.ComponentModel.BackgroundWorker worker)
        {
            backgroundWorker = worker;
        }

        internal ModelImportStatus DoCollarImport(string SelectedFile, string SelectedFormatBMFile, ImportDataMap importMap, RawFileReader blockRawFileReader, Guid XODBProjectID, bool overwrite)
        {
          
            
            BaseImportTools bit = new BaseImportTools();
            // get the current collar names in this project
            List<CollarInfo> existingHoles = this.GetHolesForProject(XODBProjectID);


            List<string> existingHoleNames = new List<string>();
            foreach (CollarInfo ci in existingHoles)
            {
                existingHoleNames.Add(ci.Name);
            }

            ModelImportStatus mos = new ModelImportStatus();
            Stream fileStream = new FileStream(SelectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            //Stream fileStream = new FileStream(SelectedFile, FileMode.Open);
            bit.PerformCollarImport(mos, fileStream, null, importMap, this.backgroundWorker, XODBProjectID, ConnectionString, existingHoleNames, overwrite);
            return mos;
            
        }

        internal ModelImportStatus DoAssayImport(string SelectedFile, string SelectedFormatFile, ImportDataMap importMap, RawFileReader blockRawFileReader, Guid XODBProjectID, bool checkForDuplicates, bool doImportOverwrite)
        {
            BaseImportTools bit = new BaseImportTools();
            ModelImportStatus mos = new ModelImportStatus();
            
            GeneralFileInfo gfi = new GeneralFileInfo();
            gfi.GeneralFileStats(SelectedFile);
            int numLines = gfi.numLines;

            Stream fileStream = new FileStream(SelectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //Stream fileStream = new FileStream(SelectedFile, FileMode.Open);
            bit.PerformAssayImport(mos, fileStream, null, importMap, this.backgroundWorker, XODBProjectID, ConnectionString, numLines, checkForDuplicates, doImportOverwrite);
            return mos;
        }


        internal ModelImportStatus DoSurveyImport(string SelectedFile, string SelectedFormatFile, ImportDataMap importMap, RawFileReader blockRawFileReader, Guid XODBProjectID, bool doOverwrite, bool checkForDuplicates)
        {
            BaseImportTools bit = new BaseImportTools();
            ModelImportStatus mos = new ModelImportStatus();

            GeneralFileInfo gfi = new GeneralFileInfo();
            gfi.GeneralFileStats(SelectedFile);
            int numLines = gfi.numLines;

            Stream fileStream = new FileStream(SelectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //Stream fileStream = new FileStream(SelectedFile, FileMode.Open);
            bit.PerformSurveyImport(mos, fileStream, null, importMap, this.backgroundWorker, XODBProjectID, ConnectionString, numLines, doOverwrite, checkForDuplicates);
            return mos;
        }

        internal Dictionary<Guid, string> GetProjectList()
        {
            BaseImportTools bit = new BaseImportTools();
            return bit.GetProjectsList();
        }

        internal List<CollarInfo> GetHolesForProject(Guid currentSelectedProject)
        {

            BaseImportTools bit = new BaseImportTools();
            List<CollarInfo> collarNameList = bit.FindCollarsInProject(currentSelectedProject);
            return collarNameList;
        }

        internal ModelImportStatus DoLithoImport(string SelectedFile, string SelectedFormatFile, ImportDataMap importMap, RawFileReader blockRawFileReader, Guid XODBProjectID, bool doOverwrite, bool checkForDuplicates)
        {
            BaseImportTools bit = new BaseImportTools();
            ModelImportStatus mos = new ModelImportStatus();

            GeneralFileInfo gfi = new GeneralFileInfo();
            gfi.GeneralFileStats(SelectedFile);
            int numLines = gfi.numLines;


            //Stream fileStream = new FileStream(SelectedFile, FileMode.Open);
            Stream fileStream = new FileStream(SelectedFile, FileMode.Open, FileAccess.Read , FileShare.ReadWrite);
            bit.PerformLithoImport(mos, fileStream, null, importMap, this.backgroundWorker, XODBProjectID, ConnectionString, numLines, doOverwrite, checkForDuplicates);
            return mos;
        }

        internal ModelImportStatus BatchImportLasFiles(string[] filePaths, Guid currentProjectID)
        {

            ModelImportStatus finalStatus = new ModelImportStatus();

            LASBatchImportTools ll = new LASBatchImportTools();
            List<string> messages = new List<string>();
            int importCount = 0;
            int failCount = 0;
            string report = "";
            Dictionary<string, ModelImportStatus> mosList = new Dictionary<string, ModelImportStatus>();
            bool reportStatus = false;
            if (this.backgroundWorker != null)
            {
                reportStatus = true;
            }
            

            int fileCount = filePaths.Length;
            int thisFileNum = 0;

            foreach (string file in filePaths)
            { 
                double pct = ((double)thisFileNum / (double)fileCount) * 100.0;
                thisFileNum++; 
               
                
                if (reportStatus)
                {
                    backgroundWorker.ReportProgress((int)pct, "Processing las file "+thisFileNum+" of "+fileCount+", "+file);
                }

                ModelImportStatus mis = new ModelImportStatus();

                XODB.Import.Client.Processing.LASImport li = new XODB.Import.Client.Processing.LASImport();
                LASFile lf = li.GetLASFile(file, mis);
                if (lf == null)
                {
                    
                    mis.errorMessages.Add("Failed to load LAS file " + file);
                    mosList.Add(file, mis);
                    continue;
                }

                string msg = ll.ProcessLASFile(lf, file, mis, currentProjectID, this.backgroundWorker);
                if (msg != null)
                {
                    messages.Add(msg);
                    report += msg + "\n";
                    failCount++;

                }
                else
                {
                    importCount++;
                }

                mosList.Add(file, mis);

            }

            string finalReport = "Immport status:\nFiles imported:" + importCount + "\nFailed files:" + failCount + "\n\nMessages:\n";

            finalReport += report;
            int totRecordsAddedCount = 0;
            int totLinesReadCount = 0;
            foreach (KeyValuePair<string, ModelImportStatus> kvp in mosList) {
                string lfName = kvp.Key;
                ModelImportStatus ms = kvp.Value;
                totRecordsAddedCount += ms.recordsAdded;
                totLinesReadCount += ms.linesReadFromSource;
                if (ms.finalErrorCode != ModelImportStatus.OK) {
                    finalStatus.finalErrorCode = ModelImportStatus.GENERAL_LOAD_ERROR;

                }

                foreach (string m in ms.warningMessages) {
                    finalStatus.warningMessages.Add(m);
                }
                foreach (string m in ms.errorMessages)
                {
                    finalStatus.errorMessages.Add(m);
                }

            
            }

            finalStatus.linesReadFromSource = totLinesReadCount;
            finalStatus.recordsAdded = totRecordsAddedCount;

            return finalStatus;

        }
    }
}
