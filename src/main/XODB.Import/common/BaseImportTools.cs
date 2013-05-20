using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XODB.Import;
using XODB.Import.ColumnSpecs;
using XODB.Import.DataModels;
using XODB.Import.DataWrappers;
using XODB.Import.FormatSpecification;
using XODB.Import.ImportUtils;

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
           ImportUtils.BlockImport dbIm = new ImportUtils.BlockImport();
           
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
            this.currentWorker = worker;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connString;
            // talk to the import lib to do the import
            
            DateTime startTime = DateTime.Now;
            int batchSize = 1000;
            //UpdateStatus("Creating new XODB block model", 20.0);
            ImportUtils.BlockImport dbIm = null;
            try
            {
                dbIm = new ImportUtils.BlockImport();
                //ImportDataMap importMapLoaded = FormatSpecificationIO.ImportMapIO.LoadImportMap(ffFileStream);
                X_BlockModel xAdd = new X_BlockModel();
                xAdd.OriginX = (Decimal)xOrigin;                                   // TO-DO
                xAdd.OriginY = (Decimal)yOrigin;                                   // TO-DO
                xAdd.OriginZ = (Decimal)zOrigin;                                   // TO-DO
                xAdd.Alias = alias;
				// when on server, automatically pick up the author GUID and apply it to the model.
				if(currentWorker == null){                                
			    	xAdd.AuthorContactID = authorGuid;
                	xAdd.ResponsibleContactID = authorGuid;
				}
                xAdd.VersionUpdated = DateTime.UtcNow;

                xAdd.BlockModelID = blockModelGUID;
                xAdd.ProjectID = new Guid(XODBProjectID);       // TODO - allow user to pick size
                resourceModels.X_BlockModel.Add(xAdd);
                resourceModels.SaveChanges();
                UpdateStatus("Setting model meta data", 25.0);
                // add the meta data to identify all of the oclumns etc.
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
                }
                catch (Exception ex) {
                    mos.AddErrorMessage("Error setting block model meta data:\n" + ex.ToString());
                }
                try
                {
                    // add the new BM guid to the column map as a default so that it is always entered
                    importMap.columnMap.Add(new ColumnMap("", -1, "X_BlockModelBlock", "BlockModelID", ImportDataMap.TEXTDATATYPE, blockModelGUID.ToString(), blockModelGUID.ToString(), ImportDataMap.UNIT_NONE ));
                    // add the individual blocks
                    domains = dbIm.AddBlockData(mos, bmFileStream, importMap, blockModelGUID, batchSize, UpdateStatus, approxNumLines, connString);
					// run this only if in wonows client (determined by the status of the worker thread at this stage)
					if(currentWorker != null){                   
					    List<Tuple<string, string>> doms = new List<Tuple<string, string>>();
	                    string domainColumnName = "Domain";
	                    foreach (string ss in domains)
	                    {
	                        doms.Add(new Tuple<string, string>(domainColumnName, ss));
	                    }
	                    dbIm.UpdateDomains(doms, blockModelGUID);
					}
                }
                catch (Exception ex) {
                    mos.AddErrorMessage("Error adding block data:\n" + ex.ToString());
                }
                
            }
            return domains;
        }
		
		
        public void PerformCollarImport(ModelImportStatus mos, System.IO.Stream bmFileStream, System.IO.Stream ffFileStream, ImportDataMap importMap, System.ComponentModel.BackgroundWorker backgroundWorker, Guid XODBProjectID, string connectionString, List<string> existingHoleNames, bool overwrite)
        {

            
            this.currentWorker = null;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connectionString;
            // talk to the import lib to do the import

            DateTime startTime = DateTime.Now;
            int batchSize = 1000;
            //UpdateStatus("Creating new XODB block model", 20.0);
            ImportUtils.CollarImport collImp = null;

            collImp = new ImportUtils.CollarImport();
            int approxNumLines = 100;

            importMap.columnMap.Add(new ColumnMap("", -1, "X_Header", "ProjectID", ImportDataMap.TEXTDATATYPE, XODBProjectID.ToString(), XODBProjectID.ToString(), ImportDataMap.UNIT_NONE));
            collImp.AddCollarData(mos, bmFileStream, importMap, batchSize, UpdateStatus, approxNumLines, connectionString, existingHoleNames, XODBProjectID, overwrite);
            


        }



        public void PerformAssayImport(ModelImportStatus mos, System.IO.Stream bmFileStream, System.IO.Stream ffFileStream, 
                                        ImportDataMap importMap, System.ComponentModel.BackgroundWorker backgroundWorker, Guid XODBProjectID, 
                                        string connectionString, int numLines,bool checkForDuplicates, bool doImportOverwrite)
        {
            this.currentWorker = backgroundWorker;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connectionString;
            // talk to the import lib to do the import
            DateTime startTime = DateTime.Now;
            int batchSize = 100;
            //UpdateStatus("Creating new XODB block model", 20.0);
            ImportUtils.AssayImport assImp = null;
            assImp = new ImportUtils.AssayImport();
            assImp.AddAssayData(mos, bmFileStream, importMap, batchSize, UpdateStatus, numLines, connectionString, XODBProjectID, checkForDuplicates, doImportOverwrite);


        }

        public void PerformSurveyImport(ModelImportStatus mos, System.IO.Stream bmFileStream, System.IO.Stream ffFileStream, ImportDataMap importMap, System.ComponentModel.BackgroundWorker backgroundWorker, Guid XODBProjectID, string connectionString, int numLines, bool doOverwrite, bool checkForDuplicates)
        {
            this.currentWorker = backgroundWorker;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connectionString;
            // talk to the import lib to do the import
            DateTime startTime = DateTime.Now;
            int batchSize = 100;
            //UpdateStatus("Creating new XODB block model", 20.0);
            ImportUtils.SurveyImport surImp = null;
            surImp = new ImportUtils.SurveyImport();
            surImp.AddSurveyData(mos, bmFileStream, importMap, batchSize, UpdateStatus, numLines, connectionString, XODBProjectID, doOverwrite, checkForDuplicates);


        }


        public void PerformLithoImport(ModelImportStatus mos, System.IO.Stream bmFileStream, System.IO.Stream ffFileStream, ImportDataMap importMap, System.ComponentModel.BackgroundWorker backgroundWorker, Guid XODBProjectID, string connectionString, int numLines, bool doOverwrite, bool checkForDuplicates)
        {
            this.currentWorker = backgroundWorker;
            XODBImportEntities resourceModels = new XODBImportEntities();
            resourceModels.Database.Connection.ConnectionString = connectionString;
            // talk to the import lib to do the import
            DateTime startTime = DateTime.Now;
            int batchSize = 100;
            //UpdateStatus("Creating new XODB block model", 20.0);
            ImportUtils.LithoImport lithImp = null;
            lithImp = new ImportUtils.LithoImport();
            lithImp.AddLithoData(mos, bmFileStream, importMap, batchSize, UpdateStatus, numLines, connectionString, XODBProjectID, doOverwrite, checkForDuplicates);
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
            ImportUtils.BlockImport dbIm = new ImportUtils.BlockImport();
            ImportDataMap idm = new ImportDataMap();
            idm.columnMap = new List<ColumnMap>();
            idm.columnMap.Add(new ColumnMap(columnNameToImport, columnIndexToImport, "X_BlockModelBlock", colToInsertTo,ImportDataMap.NUMERICDATATYPE, null, null, null));
            dbIm.SetBlockModelMetaData(bmGuid, idm, connString);

            return dbIm.UpdateBlockData(bmStream, bmGuid, colToInsertTo, connString);
            

        }

        public List<ColumnMetaInfo> GetGeophysicsColumns()
        {
            //TODO Go away to database schema and get the columns
            

            List<ColumnMetaInfo> colList = new List<ColumnMetaInfo>();
            colList.Add(new ColumnMetaInfo() { columnName = "FROM" });
            colList.Add(new ColumnMetaInfo() { columnName = "[RESULT]" });
            

            return colList;
        }



        public List<ColumnMetaInfo> GetAssayColumns(string connectionString)
        {
            List<ColumnMetaInfo> colList = new List<ColumnMetaInfo>();

            List<FKSpecification> fkList = ForeignKeyUtils.QueryForeignKeyRelationships(connectionString, "X_Sample");

            X_Sample xag = new X_Sample();
            QueryColumnData(colList, fkList, xag);
            X_AssayGroupTestResult xtr = new X_AssayGroupTestResult();
            fkList = ForeignKeyUtils.QueryForeignKeyRelationships(connectionString, "X_AssayGroupTestResult");
            QueryColumnData(colList, fkList, xtr);

            List<string> removeStubs = new List<string>();
            removeStubs.Add("X_Sample");
            removeStubs.Add("X_Assay");
            removeStubs.Add("Version");
            removeStubs.Add("X_Dict");
            List<ColumnMetaInfo> colListP = new List<ColumnMetaInfo>();
            colListP = PruneColumnList(removeStubs, colList);
            ColumnMetaInfo ci = new ColumnMetaInfo();
            ci.columnName = "[ASSAY RESULT]";
            ci.fkSpec = null;
            colListP.Insert(0,ci);

            // now mark only the mandatory fields - for assay this will be header id, from, to and result
            foreach (ColumnMetaInfo c in colListP) {
                
                if (c.columnName.Equals("HeaderID")) {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("FromDepth")) {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("ToDepth")) {
                    c.isMandatory = true;
                }
            }

            return colListP;
        }

        private List<ColumnMetaInfo> PruneColumnList(List<string> removeStubs, List<ColumnMetaInfo> colList)
        {
            List<ColumnMetaInfo> colListP = new List<ColumnMetaInfo>();
            
            foreach (ColumnMetaInfo cim in colList) {
                string colName = cim.columnName;
                bool doExclusion = false;
                foreach (string rem in removeStubs) {

                    if (colName.Trim().StartsWith(rem.Trim()))
                    {
                        doExclusion = true;
                        break;
                    }
                    

                }
                if (!doExclusion)
                {
                    colListP.Add(cim);
                }
            }
            return colListP;
        }

        private void QueryColumnData(List<ColumnMetaInfo> colList, List<FKSpecification> fkList, object xag)
        {
            foreach (PropertyInfo pi in xag.GetType().GetProperties())
            {

                MemberTypes mt = pi.MemberType;
                string val = mt.ToString();
                PropertyAttributes patt = pi.Attributes;
                string nm = pi.PropertyType.Name;
                String name = pi.Name;
                Type rt = pi.GetMethod.ReturnType;
                string tName = GuessMainType(rt.FullName);

                FKSpecification fkSpec = null;

                bool hasFK = LookupFKForColumn(name, out fkSpec, fkList);
                ColumnMetaInfo cmi = new ColumnMetaInfo() { columnName = name, fkSpec = fkSpec, hasFK = hasFK, dbTypeName = tName };
                colList.Add(cmi);
            }
        }

        public string GuessMainType(string typeFromEDM) {
            string rt = ImportDataMap.NUMERICDATATYPE;
            
            if (typeFromEDM.ToLower().Contains("system.string")) {
                rt = ImportDataMap.TEXTDATATYPE;
            }
            else if (typeFromEDM.ToLower().Contains("system.datetime"))
            {
                rt = ImportDataMap.TIMESTAMPDATATYPE;// "TIMESTAMP";
            }
            return rt;
        
        }

        public List<ColumnMetaInfo> GetCollarColumns(string connString)
        {
            X_Header ob = new X_Header();
            XODBImportCollarEntities e = new XODBImportCollarEntities();
            string tableName = "X_Header";
            List<ColumnMetaInfo> colList = this.QueryColumns(connString, ob, tableName);

            List<string> removeStubs = new List<string>();
            removeStubs.Add("HeaderID");
            removeStubs.Add("ProjectID");
            removeStubs.Add("X_Header");
            removeStubs.Add("Version");
            removeStubs.Add("X_Dict");
            List<ColumnMetaInfo> colListP = new List<ColumnMetaInfo>();
            colListP = PruneColumnList(removeStubs, colList);

            // now mark only the mandatory fields - for assay this will be header id, from, to and result
            foreach (ColumnMetaInfo c in colListP)
            {

                if (c.columnName.Equals("HoleName"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("Easting"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("Northing"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("Elevation"))
                {
                    c.isMandatory = true;
                }
            }

            return colListP;
        }


        public List<ColumnMetaInfo> GetSurveyColumns(string connString)
        {
            X_Survey ob = new X_Survey();
            XODBImportCollarEntities e = new XODBImportCollarEntities();

            string tableName = "X_Survey";

            List<ColumnMetaInfo> colList = QueryColumns(connString, ob, tableName);

            List<string> removeStubs = new List<string>();
            removeStubs.Add("X_Survey");
            removeStubs.Add("Version");
            removeStubs.Add("X_Dict");
            List<ColumnMetaInfo> colListP = new List<ColumnMetaInfo>();
            colListP = PruneColumnList(removeStubs, colList);

            // now mark only the mandatory fields - for assay this will be header id, from, to and result
            foreach (ColumnMetaInfo c in colListP)
            {

                if (c.columnName.Equals("HeaderID"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("Depth"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("Dip"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("OriginalAzimuth"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("CorrectedAzimuth"))
                {
                    c.isMandatory = true;
                }
            }

            return colListP;
        }


        public List<ColumnMetaInfo> GetLithoColumns(string connString)
        {
            X_Lithology ob = new X_Lithology();
            string tableName = "X_Lithology";

            XODBImportCollarEntities e = new XODBImportCollarEntities();

            List<ColumnMetaInfo> colList = QueryColumns(connString, ob, tableName);


            List<string> removeStubs = new List<string>();
            removeStubs.Add("X_Litho");
            removeStubs.Add("Version");
            removeStubs.Add("X_Dict");
            List<ColumnMetaInfo> colListP = new List<ColumnMetaInfo>();
            colListP = PruneColumnList(removeStubs, colList);

            // now mark only the mandatory fields - for assay this will be header id, from, to and result
            foreach (ColumnMetaInfo c in colListP)
            {

                if (c.columnName.Equals("HeaderID"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("FromDepth"))
                {
                    c.isMandatory = true;
                }
                else if (c.columnName.Equals("ToDepth"))
                {
                    c.isMandatory = true;
                }

            }

            return colListP;
        }

        private List<ColumnMetaInfo> QueryColumns(string connString, object ob, string tableName)
        {
            
            List<FKSpecification> fkList = ForeignKeyUtils.QueryForeignKeyRelationships(connString, tableName);
            List<ColumnMetaInfo> colList = new List<ColumnMetaInfo>();
            foreach (PropertyInfo pi in ob.GetType().GetProperties())
            {
                MemberTypes mt = pi.MemberType;
                string val = mt.ToString();
                PropertyAttributes patt = pi.Attributes;
                string nm = pi.PropertyType.Name;
                String name = pi.Name;
                FKSpecification fkSpec = null;
                bool hasFK = LookupFKForColumn(name, out fkSpec, fkList);
                Type rt = pi.GetMethod.ReturnType;
                string tName = GuessMainType(rt.FullName);
                ColumnMetaInfo cmi = new ColumnMetaInfo() { columnName = name, fkSpec = fkSpec, hasFK = hasFK, dbTypeName = tName };
                colList.Add(cmi);
            }
            return colList;
        }

        private bool LookupFKForColumn(string name, out FKSpecification fkSpec, List<FKSpecification> fkList)
        {
            fkSpec = null;
            bool hasFK = false;
            foreach (FKSpecification f in fkList) {
                if (name.Trim().Equals(f.parentColumnName.Trim())) {
                    hasFK = true;
                    fkSpec = f;
                    break;
                }
            }

            return hasFK;
        }

        public Dictionary<Guid, string> GetProjectsList()
        {
            return ProjectUtils.GetProjectList();
        }

        public List<CollarInfo> FindCollarsInProject(Guid currentSelectedProject)
        {

            List<CollarInfo> collars = CollarQueries.FindCollarsForProject(currentSelectedProject);
            return collars;
        }
    }

}
