using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XODB.Import;
using XODB.Import.DataModels;
using XODB.Import.FormatSpecification;

namespace XODB.Import
{
    public class BaseImportTools
    {
        System.ComponentModel.BackgroundWorker currentWorker = null;


        public BaseImportTools() { }

        public string TestConnection(string connString) {

            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connString;
            // talk to the import lib to do the import
            DbSet<X_BlockModel> models = resourceModels.X_BlockModel;
            var query = from X_BlockModel in models select new { X_BlockModel.BlockModelID, X_BlockModel.OriginX, X_BlockModel.OriginY, X_BlockModel.OriginZ, X_BlockModel.ProjectID };

            foreach (X_BlockModel bm in models)
            {
                Guid gu = bm.BlockModelID;
                string alias = bm.Alias;
                int proj = bm.Version;
            }

            return "In XODB.Import";
        }

        public List<string> GetBMColumns()
        {
            List<string> cols = new List<string>();
            //For each field in the database (or property in Linq object)
            X_BlockModelBlock ob = new X_BlockModelBlock();
            
            foreach (PropertyInfo pi in ob.GetType().GetProperties())
            {

                Type ty = pi.GetType();
                String name = pi.Name;

                cols.Add(name);
            }

            return cols;

        }


        public string PerformBMImport(string bmDataFile, string selectedFormatBMFile, ImportDataMap importMap, double xOrigin, double yOrigin, double zOrigin, System.ComponentModel.BackgroundWorker worker, int approxNumLines, string XODBProjectID, string units, string connString)
        {
            this.currentWorker = worker;
            UpdateStatus("Connecting to XODB", 10.0);
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connString;
            // talk to the import lib to do the import
            DbSet<X_BlockModel> models = resourceModels.X_BlockModel;
            var query = from X_BlockModel in models select new { X_BlockModel.BlockModelID, X_BlockModel.OriginX, X_BlockModel.OriginY, X_BlockModel.OriginZ, X_BlockModel.ProjectID };

          
            List<string> cn = new List<string>();

            //For each field in the database (or property in Linq object)
            X_BlockModel ob = new X_BlockModel();
            foreach (PropertyInfo pi in ob.GetType().GetProperties())
            {
                Type ty = pi.GetType();
                String name = pi.Name;
                cn.Add(name);
            }


          
           DateTime startTime = DateTime.Now;
           int batchSize = 100;
           UpdateStatus( "Creating new XODB block model", 20.0);
           BlockImportUtils.BlockImport dbIm = new BlockImportUtils.BlockImport();
           
           Guid blockModelGUID = Guid.NewGuid();

           X_BlockModel xAdd = new X_BlockModel();
           xAdd.OriginX = (Decimal)xOrigin;                                   // TO-DO
           xAdd.OriginY = (Decimal)yOrigin;                                   // TO-DO
           xAdd.OriginZ = (Decimal)zOrigin;                                   // TO-DO


           xAdd.BlockModelID = blockModelGUID;
           xAdd.ProjectID = new Guid(XODBProjectID);       // TODO - allow user to pick size
           resourceModels.X_BlockModel.Add(xAdd);
           resourceModels.SaveChanges();
           UpdateStatus( "Setting model meta data", 25.0);
           // add the meta data to identify all of the oclumns etc.
           List<X_BlockModelMetadata> blockColumnMetaData = dbIm.SetBlockModelMetaData(blockModelGUID, importMap, connString);

           // add the new BM guid to the column map as a default so that it is always entered
           importMap.columnMap.Add(new ColumnMap("", -1, "X_BlockModelBlock", "BlockModelID", ImportDataMap.TEXTDATATYPE, blockModelGUID.ToString(), null, units));
            
           // add the individual blocks
           dbIm.AddBlockData(bmDataFile, importMap, blockModelGUID, batchSize, UpdateStatus, approxNumLines, connString);
           //dbIm.AddBlockDataNorm(bmDataFile, importMap, blockModelGUID, batchSize, UpdateStatus, approxNumLines, blockColumnMetaData);

           DateTime endTime = DateTime.Now;
           long compVal = (endTime.Ticks - startTime.Ticks) / 1000;
           string message = "" + startTime.ToShortTimeString() + " Ended: " + endTime.ToShortTimeString();

           long xval = compVal;

            return "";
        }


        public List<string> PerformBMImport(ModelImportStatus mos, Guid blockModelGUID, System.IO.Stream bmFileStream, System.IO.Stream ffFileStream, ImportDataMap importMap, double xOrigin, double yOrigin, double zOrigin, System.ComponentModel.BackgroundWorker worker, int approxNumLines, string XODBProjectID, string alias, Guid authorGuid, string connString)
        {
            this.currentWorker = null;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connString;
            // talk to the import lib to do the import
            
            DateTime startTime = DateTime.Now;
            int batchSize = 1000;
            //UpdateStatus("Creating new XODB block model", 20.0);
            BlockImportUtils.BlockImport dbIm = null;
            try
            {
                dbIm = new BlockImportUtils.BlockImport();
                //ImportDataMap importMapLoaded = FormatSpecificationIO.ImportMapIO.LoadImportMap(ffFileStream);
                X_BlockModel xAdd = new X_BlockModel();
                xAdd.OriginX = (Decimal)xOrigin;                                   // TO-DO
                xAdd.OriginY = (Decimal)yOrigin;                                   // TO-DO
                xAdd.OriginZ = (Decimal)zOrigin;                                   // TO-DO
                xAdd.Alias = alias;
                xAdd.AuthorContactID = authorGuid;
                xAdd.ResponsibleContactID = authorGuid;
                xAdd.VersionUpdated = DateTime.Now;

                xAdd.BlockModelID = blockModelGUID;
                xAdd.ProjectID = new Guid(XODBProjectID);       // TODO - allow user to pick size
                resourceModels.X_BlockModel.Add(xAdd);
                resourceModels.SaveChanges();
                UpdateStatus("Setting model meta data", 25.0);
                // add the meta data to identify all of the oclumns etc.
                //mos.RecordsImported++;
            }
            catch (Exception ex) {
                mos.AddErrorMessage("Error setting block model defintion data. "+ex.ToString());
            }
            List<string> domains = new List<string>();
            if (dbIm != null)
            {
                try
                {
                    List<X_BlockModelMetadata> blockColumnMetaData = dbIm.SetBlockModelMetaData(blockModelGUID, importMap, connString);
                    //mos.RecordsImported += blockColumnMetaData.Count;
                }
                catch (Exception ex) {
                    mos.AddErrorMessage("Error setting block model meta data:\n" + ex.ToString());
                }
                try
                {
                    // add the new BM guid to the column map as a default so that it is always entered
                    importMap.columnMap.Add(new ColumnMap("", -1, "X_BlockModelBlock", "BlockModelID", ImportDataMap.TEXTDATATYPE, blockModelGUID.ToString(), null, ImportDataMap.UNIT_NONE));
                    // add the individual blocks
                    domains = dbIm.AddBlockData(mos, bmFileStream, importMap, blockModelGUID, batchSize, UpdateStatus, approxNumLines, connString);
                }
                catch (Exception ex) {
                    mos.AddErrorMessage("Error adding block data:\n" + ex.ToString());
                }
                
            }
            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="pct"></param>
        private void UpdateStatus(string message, double pct)
        {
            if (currentWorker != null)
            {
                currentWorker.ReportProgress((int)pct, message);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerLine"></param>
        /// <param name="firstDataLine"></param>
        /// <param name="_originX"></param>
        /// <param name="_originY"></param>
        /// <param name="_originZ"></param>
        public void ParseDataLinesForOrigins(string headerLine, string firstDataLine, out double _originX, out double _originY, out double _originZ)
        {
            char delim = ',';
            string[] headerItems = headerLine.Split(new char[] { delim }, StringSplitOptions.None);
            string[] lineItems = firstDataLine.Split(new char[] { delim }, StringSplitOptions.None);
            _originX = 0;
            _originY = 0;
            _originZ = 0;
         
            int index = -1;
            
            index = FindItemInLine(headerItems, "XMORIG");
            if (index > -1)
            {
                double.TryParse(lineItems[index], out _originX);
            }
            index = FindItemInLine(headerItems, "YMORIG");
            if (index > -1)
            {
                double.TryParse(lineItems[index], out _originY);
            }
            
            index = FindItemInLine(headerItems, "ZMORIG");
            if (index > -1)
            {
                double.TryParse(lineItems[index], out _originZ);
            }

         
        }

        private static int FindItemInLine(string[] headerItems, string find)
        {
            int ct = 0;
            int index = -1;
            foreach (string s in headerItems)
            {
                if (s.Trim().Equals(find))
                {
                    index = ct;
                    break;
                }
                ct++;
            }
            return index;
        }



        // attempt to use the block model file header to automatically create a defintion based on goldfields typical model formats
        public ImportDataMap AutoGenerateFormatDefinition(string headerLine)
        {
            char delim = ',';
                        
            Dictionary<string, bool> autoMap = new Dictionary<string, bool>();
            string[] headerItems = headerLine.Split(new char[] { delim }, StringSplitOptions.None);
            // iterate through each item in the header and assign it to a target column. 
            foreach(string ss in headerItems){
                autoMap.Add(ss, false);
            }

            // manually here get the core BM fields.
            // DB field = [Domain]
            ImportDataMap idm = new ImportDataMap();
            idm.columnMap = new List<ColumnMap>();
            idm.inputDelimiter = ',';
            idm.mapTargetPrimaryTable = "X_BlockModelBlock";
            idm.dataStartLine = 2;

            List<string> dmFields = new List<string>();

            int idx = -1;   
            
            string dbArea = "BlockModel";

            idx = AutoGenColMap(headerItems, idm,  "DOMAIN", "Domain", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "XC", "CentroidX", dbArea, autoMap);     
            idx = AutoGenColMap(headerItems, idm, "YC", "CentroidY", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "ZC", "CentroidZ", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "XINC", "LengthX", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "YINC", "LengthY", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "ZINC", "LengthZ", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "DENSITY", "Density", dbArea, autoMap);
            idx = AutoGenColMap(headerItems, idm, "RESCAT", "ResourceCategory", dbArea, autoMap);
            int num = 1;
            // auto map all ofther fields into numeric 1 to n
            foreach (KeyValuePair<string, bool> kvp in autoMap) {
                if ((bool)kvp.Value == false) {
                    string nm = kvp.Key;
                    // make a column map for this stright into the numeric, and keep track of numeric number
                    string targetFieldName = "Numeric" + num;
                    AutoGenColMap(headerItems, idm, nm, targetFieldName, dbArea, null);
                    num++;
                }
            }

            return idm;

        }

        private static int AutoGenColMap(string[] headerItems, ImportDataMap idm, string sourceName, string targetName, string dbArea, Dictionary<string, bool> autoMap)
        {
            int idx = FindItemInLine(headerItems, sourceName);
            if(idx > -1){
                
                idm.columnMap.Add(new ColumnMap(sourceName, idx, dbArea, targetName, ImportDataMap.NUMERICDATATYPE, null, null, null));
                if (autoMap != null) { autoMap[sourceName] = true; }
            }
            return idx;
        }

        public ModelImportStatus PerformBMAppend(System.IO.Stream bmStream, Guid bmGuid, string alias, string columnNameToImport, int columnIndexToImport, string connString)
        {
            // TODO: read stream and write updates to database

            // get the next column to write to - search meta data to get the list of occupied columns
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connString;
            List<X_BlockModelMetadata> d = new List<X_BlockModelMetadata>();
            var o = resourceModels.X_BlockModelMetadata.Where(f => f.BlockModelID == bmGuid && f.IsColumnData == true).Select(f => (string)f.BlockModelMetadataText).ToArray();
            // yuk, ugly hack to get the next column to update into.  In the long run, use normalised data as it will be much easier
            int lastIndex = 0;
            foreach (string s in o) {
                if (s.StartsWith("Numeric")) {
                    string endBit = s.Substring(7);
                    int ival = -1;
                    bool parsed = int.TryParse(endBit, out ival);
                    if (parsed) {
                       lastIndex = Math.Max(ival, lastIndex);
                    }

                }
            }
            string colToInsertTo = "Numeric"+(lastIndex+1);
            //TODO: add this new meta data item into the database

            //TODO: update the data within the database itself
            BlockImportUtils.BlockImport dbIm = new BlockImportUtils.BlockImport();
            ImportDataMap idm = new ImportDataMap();
            idm.columnMap = new List<ColumnMap>();
            idm.columnMap.Add(new ColumnMap(columnNameToImport, columnIndexToImport, "X_BlockModelBlock", colToInsertTo,ImportDataMap.NUMERICDATATYPE, null, null, null));
            dbIm.SetBlockModelMetaData(bmGuid, idm, connString);

            return dbIm.UpdateBlockData(bmStream, bmGuid, colToInsertTo, connString);
            

        }
    }

}
