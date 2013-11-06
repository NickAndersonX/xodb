﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XODB.Module.BusinessObjects;
using XODB.Import.FormatSpecification;

namespace XODB.Import.ImportUtils
{
    public class CoalQualityImport
    {

        public CoalQualityImport() { }

        internal void AddCoalQualityData(ModelImportStatus mos, Stream fileStream, FormatSpecification.ImportDataMap importMap,
                                    int batchSize, Action<string, double> UpdateStatus, int approxNumLines, 
                                    string connectionString, Guid XODBProjectID, bool checkForDuplicates, bool doImportOverwrite)
        {
            bool commitToDB = false;
            DateTime currentUpdateTimestamp = DateTime.UtcNow;
            // first set up an assay group object - we can do this through the edm
            using (var entityObj = new XODBC(connectionString, null))
            {
                Guid agGuid = Guid.NewGuid();
                AssayGroup ag = new AssayGroup();
                ag.AssayGroupID = agGuid;
                ag.ProjectID = XODBProjectID;
                ag.AssayGroupName = "Manual import";
                ag.Comment = "From file " + importMap.mapOriginalDataFile;
                ag.Entered = currentUpdateTimestamp;
                ag.VersionUpdated = currentUpdateTimestamp;
                entityObj.AssayGroups.AddObject(ag);
                if (commitToDB)
                {
                    entityObj.SaveChanges();
                }

                // set up the assay test columns - one of these for each test type
                Dictionary<ColumnMap, Guid> resultsColumns = new Dictionary<ColumnMap, Guid>();
                Dictionary<Guid, AssayGroupTest> assayGroups = new Dictionary<Guid, AssayGroupTest>();
                ColumnMap cmProgram = null;
                ColumnMap cmStage = null;
                ColumnMap cmSizeFraction = null;
                ColumnMap cmWashFraction = null;
                foreach (ColumnMap cim in importMap.columnMap)
                {
                    if (cim.targetColumnName.Trim().StartsWith("[ASSAY"))
                    {
                        // this is a test category
                        resultsColumns.Add(cim, Guid.NewGuid());
                    }
                    else if (cim.targetColumnName.Trim().StartsWith("[PROGRAM"))
                    {
                        cmProgram = cim;
                    }
                    else if (cim.targetColumnName.Trim().StartsWith("[STAGE"))
                    {
                        cmStage = cim;
                    }
                    else if (cim.targetColumnName.Trim().StartsWith("[SIZE FRACTION"))
                    {
                        cmSizeFraction = cim;
                    }
                    else if (cim.targetColumnName.Trim().StartsWith("[WASH FRACTION"))
                    {
                        cmWashFraction = cim;
                    }
                }
                UpdateStatus("Setting up assay tests ", 2);

                foreach (KeyValuePair<ColumnMap, Guid> kvp in resultsColumns)
                {
                    ColumnMap cm = kvp.Key;
                    Guid g = kvp.Value;
                    AssayGroupTest xt = new AssayGroupTest();

                    string ss1 = "";
                    if (cm.sourceColumnName != null && cm.sourceColumnName.Length > 15)
                    {
                        ss1 = cm.sourceColumnName.Substring(0, 16);
                    }
                    else
                    {
                        ss1 = cm.sourceColumnName;
                    }
                    Guid pid = FindParameterForAssayTypeName(cm.sourceColumnName);
                    xt.ParameterID = pid;
                    xt.AssayTestName = ss1;
                    xt.AssayGroupID = agGuid;
                    xt.AssayGroupTestID = g;
                    xt.VersionUpdated = currentUpdateTimestamp;
                    entityObj.AssayGroupTests.AddObject(xt);
                    assayGroups.Add(g, xt);
                    if (commitToDB)
                    {
                        entityObj.SaveChanges();
                    }
                }



                // iterate through the data lines
                int ct = 1;
                int linesRead = 0;
                SqlConnection connection = null;
                SqlConnection secondaryConnection = null;
                //List<string> uniqueDomains = new List<string>();
                // get a connection to the database
                try
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();

                    secondaryConnection = new SqlConnection(connectionString);
                    secondaryConnection.Open();
                    bool hasDuplicateIntervals = false;

                    SqlTransaction trans;
                    trans = connection.BeginTransaction();
                    List<SqlCommand> commands = new List<SqlCommand>();
                    int tb = 0;
                    int transactionBatchLimit = batchSize;

                    // open the filestream and read the first line
                    StreamReader sr = null;
                    try
                    {
                        sr = new StreamReader(fileStream);
                    }
                    catch (Exception ex)
                    {
                        mos.AddErrorMessage("Error getting data stream for input data:\n" + ex.ToString());
                        mos.finalErrorCode = ModelImportStatus.ERROR_LOADING_FILE;
                    }
                    string line = null;
                    float pct = 0;
                    float bct = 1;

                    // report every X records
                    int repCount = 0;
                    float fNumLines = (float)approxNumLines;


                    Dictionary<string, Guid> holeIDLookups = new Dictionary<string, Guid>();
                    Dictionary<string, int> columnIDX = new Dictionary<string, int>();
                    int fkLookupCount = 0;

                    PopulateCMapShortcut("HeaderID", importMap, columnIDX);
                    PopulateCMapShortcut("FromDepth", importMap, columnIDX);
                    PopulateCMapShortcut("ToDepth", importMap, columnIDX);
                    PopulateCMapShortcut("SampleNumber", importMap, columnIDX);
                    PopulateCMapShortcut("LabSampleNumber", importMap, columnIDX);
                    PopulateCMapShortcut("LabBatchNumber", importMap, columnIDX);
                    ColumnMap headerCmap = importMap.FindItemsByTargetName("HeaderID");
                    AssayQueries assayQueries = new AssayQueries();

                    if (sr != null)
                    {
                        while ((line = sr.ReadLine()) != null)
                        {

                            repCount++;

                            pct = ((float)linesRead / (float)approxNumLines) * 100.0f;
                            bct++;
                            linesRead++;
                            if (ct >= importMap.dataStartLine)
                            {

                                // digest a row of input data 
                                List<string> items = parseTestLine(line, importMap.inputDelimiter);


                                Guid holeID = new Guid();
                                Decimal fromDepth = new Decimal(-9999999999);
                                Decimal toDepth = new Decimal(-9999999999);
                                string sampleNumber = null;
                                string labBatchNumber = null;
                                string labsampleNumber = null;

                                // find mapped values by name
                                int idxVal = 0;
                                // -- Get the hole ID foreign key relation
                                bool foundEntry = columnIDX.TryGetValue("HeaderID", out idxVal);
                                bool foundHole = false;
                                string holeName = "";
                                if (foundEntry)
                                {

                                    string lookupByName = "HoleName";
                                    string lookupValue = items[idxVal];
                                    holeName = lookupValue;
                                    bool lv = holeIDLookups.ContainsKey(lookupValue);
                                    if (!lv)
                                    {
                                        string headerGUID = ForeignKeyUtils.FindFKValueInOther(lookupValue, headerCmap, secondaryConnection, false, lookupByName, XODBProjectID);
                                        if (headerGUID == null)
                                        {
                                            // this means we have not found the specified records in the header table
                                            // Report on issue and skip line


                                        }
                                        else
                                        {
                                            foundHole = true;
                                            holeID = new Guid(headerGUID);
                                            holeIDLookups.Add(lookupValue, holeID);
                                            fkLookupCount++;
                                        }
                                    }
                                    else
                                    {
                                        holeIDLookups.TryGetValue(lookupValue, out holeID);
                                        foundHole = true;
                                    }


                                }

                                if (!foundHole)
                                {

                                    mos.AddErrorMessage("Failed to find hole " + holeName + ".  Skipping record at line " + linesRead + ".");
                                    mos.finalErrorCode = ModelImportStatus.DATA_CONSISTENCY_ERROR;
                                    mos.recordsFailed++;
                                    continue;
                                }
                                else
                                {
                                    bool hasFrom = false;
                                    idxVal = 0;
                                    foundEntry = columnIDX.TryGetValue("FromDepth", out idxVal);
                                    if (foundEntry)
                                    {
                                        string ii = items[idxVal];
                                        Decimal val = 0;
                                        bool isOk = Decimal.TryParse(ii, out val);
                                        if (isOk)
                                        {
                                            fromDepth = val;
                                            hasFrom = true;
                                        }
                                    }

                                    bool hasTo = false;
                                    idxVal = 0;
                                    foundEntry = columnIDX.TryGetValue("ToDepth", out idxVal);
                                    if (foundEntry)
                                    {
                                        string ii = items[idxVal];
                                        Decimal val = 0;
                                        bool isOk = Decimal.TryParse(ii, out val);
                                        if (isOk)
                                        {
                                            toDepth = val;
                                            hasTo = true;
                                        }
                                    }
                                    List<Sample> duplicateList = null;
                                    bool isDuplicateInterval = false;
                                    //if (checkForDuplicates)
                                    //{
                                    //    if (hasFrom && hasTo)
                                    //    {
                                    //        // here we need to check that the hole is not duplicated
                                    //        duplicateList = assayQueries.CheckForDuplicate(holeID, fromDepth, toDepth);
                                    //        if (duplicateList.Count > 0)
                                    //        {
                                    //            isDuplicateInterval = true;
                                    //        }
                                    //    }
                                    //    if (isDuplicateInterval)
                                    //    {
                                    //        hasDuplicateIntervals = true;
                                    //        mos.AddWarningMessage("Duplicate interval for hole " + holeName + " at depth " + fromDepth + " to " + toDepth);
                                    //        UpdateStatus("Duplicate interval at " + holeName + " " + fromDepth + ", " + toDepth, pct);
                                    //        if (!doImportOverwrite)
                                    //        {
                                    //            mos.recordsFailed++;
                                    //            continue;
                                    //        }
                                    //    }
                                    //}

                                    idxVal = 0;
                                    foundEntry = columnIDX.TryGetValue("SampleNumber", out idxVal);
                                    if (foundEntry)
                                    {
                                        string ii = items[idxVal];
                                        sampleNumber = ii;

                                    }

                                    idxVal = 0;
                                    foundEntry = columnIDX.TryGetValue("SampleNumber", out idxVal);
                                    if (foundEntry)
                                    {
                                        string ii = items[idxVal];
                                        labsampleNumber = ii;

                                    }

                                    idxVal = 0;
                                    foundEntry = columnIDX.TryGetValue("SampleNumber", out idxVal);
                                    if (foundEntry)
                                    {
                                        string ii = items[idxVal];
                                        labBatchNumber = ii;
                                    }

                                    Sample xs = new Sample();
                                    if (isDuplicateInterval == true)
                                    {
                                        xs = duplicateList.First();
                                    }
                                    else
                                    {
                                        xs.SampleID = Guid.NewGuid();
                                        xs.FromDepth = fromDepth;
                                        xs.ToDepth = toDepth;
                                        xs.HeaderID = holeID;
                                        xs.VersionUpdated = currentUpdateTimestamp;

                                        entityObj.Samples.AddObject(xs);
                                    }
                                    // Now iddentify the  program, Stage, Size fraction and wash fraction

                                    // get the program text
                                    string programType = null;
                                    if (cmProgram != null) {
                                        programType = items[cmProgram.sourceColumnNumber];
                                    }
                                    string stage = null;
                                    if (cmStage != null)
                                    {
                                        stage = items[cmStage.sourceColumnNumber];
                                    }
                                    string sizeFraction = null;
                                    if (cmSizeFraction  != null)
                                    {
                                        sizeFraction = items[cmSizeFraction.sourceColumnNumber];
                                    }
                                    string washFraction = null;
                                    if (cmWashFraction != null)
                                    {
                                        washFraction = items[cmWashFraction.sourceColumnNumber];
                                    }

                                    
                                    AssayGroupWorkflow agWorkflowProgram = GetAssayGroupWorkflow(entityObj, programType, agGuid);
                                    AssayGroupWorkflow agWorkflowStage = GetAssayGroupWorkflow(entityObj, stage, agGuid);

                                    AssayGroupSubsample agSS = GetAssayGroupSubSample(entityObj, agGuid, agWorkflowProgram.WorkflowID, xs);

                                    AssayGroupSubsamplePrecondition agSizeFraction = GetAssayGroupPrecondition(entityObj, sizeFraction, agSS.AssayGroupSubsampleID);
                                    AssayGroupSubsamplePrecondition agWashFraction = GetAssayGroupPrecondition(entityObj, washFraction, agSS.AssayGroupSubsampleID);

                                    // now pick out all the mapped values
                                    // iterate over all [ASSAY RESULT] columns
                                    bool assayUpdated = false;
                                    bool assayAdded = false;
                                    foreach (KeyValuePair<ColumnMap, Guid> kvp in resultsColumns)
                                    {
                                        ColumnMap cm = kvp.Key;
                                        Guid g = kvp.Value; // this is the AssayGroupTestID

                                        AssayGroupTestResult testResult = new AssayGroupTestResult();
                                        testResult.AssayGroupSubsampleID = agSS.AssayGroupSubsampleID;
                                        testResult.AssayGroupTestResultID = Guid.NewGuid();
                                        testResult.AssayGroupTestID = g;
                                        testResult.SampleID = xs.SampleID;
                                        testResult.VersionUpdated = currentUpdateTimestamp;
                                        testResult.LabBatchNumber = labBatchNumber;
                                        //testResult.LabSampleNumber = labsampleNumber;
                                        Decimal result = new Decimal();
                                        if (items.Count >= cm.sourceColumnNumber)
                                        {
                                            bool parsedOK = Decimal.TryParse(items[cm.sourceColumnNumber], out result);
                                            if (parsedOK)
                                            {
                                                testResult.LabResult = result;
                                            }
                                            else
                                            {
                                                testResult.LabResultText = items[cm.sourceColumnNumber];
                                            }
                                        }
                                        else
                                        {
                                            mos.AddWarningMessage("Line " + linesRead + " contains too few columns to read " + cm.sourceColumnName);
                                        }

                                        entityObj.AssayGroupTestResults.AddObject(testResult);
                                        assayAdded = true;
                                    }

                                    if (assayAdded == true)
                                    {
                                        mos.recordsAdded++;
                                    }
                                    if (assayUpdated)
                                    {
                                        mos.recordsUpdated++;
                                    }
                                    tb++;
                                }
                            }

                            if (commitToDB)
                            {

                                if (tb == transactionBatchLimit)
                                {
                                    entityObj.SaveChanges();

                                    UpdateStatus("Writing assays to DB (" + ct + " entries)", pct);
                                    tb = 0;
                                }
                            }
                            ct++;                            
                        }
                        entityObj.SaveChanges();

                    }
                    if (hasDuplicateIntervals)
                    {
                        mos.finalErrorCode = ModelImportStatus.DATA_CONSISTENCY_ERROR;
                    }
                    string numFKLookups = "FK lookups " + fkLookupCount;
                    mos.linesReadFromSource = ct - 1;
                    UpdateStatus("Finished writing coal quality data to database.", 0);
                }
                catch (Exception ex)
                {
                    UpdateStatus("Error writing qualities to database ", 0);
                    mos.AddErrorMessage("Error writing data at line " + linesRead + ":\n" + ex.ToString());
                    mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
                }
                finally
                {
                    try
                    {
                        connection.Close();
                        secondaryConnection.Close();

                        fileStream.Close();
                    }
                    catch (Exception ex)
                    {
                        mos.AddErrorMessage("Error closing conenction to database:\n" + ex.ToString());
                        mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
                    }
                }


                mos.linesReadFromSource = linesRead;

            }
        }

        private AssayGroupSubsample GetAssayGroupSubSample(XODBC entityObj, Guid agGuid, Guid? workflowID, Sample originalSample)
        {
            AssayGroupSubsample agw = new AssayGroupSubsample();
            agw.AssayGroupID = agGuid;
            agw.AssayGroupSubsampleID = Guid.NewGuid();
            agw.AssayGroupWorkflowProcedureID = workflowID;
            agw.OriginalSample = originalSample;
            entityObj.AssayGroupSubsamples.AddObject(agw);
            entityObj.SaveChanges();

            
            return agw;
        }

        private AssayGroupSubsamplePrecondition GetAssayGroupPrecondition(XODBC entityObj, string preconditionName, Guid ssGuid)
        {
            AssayGroupSubsamplePrecondition agw = null;
            IQueryable<AssayGroupSubsamplePrecondition> res = entityObj.AssayGroupSubsamplePreconditions.Where(c => c.PreconditionName.Trim().Equals(preconditionName.Trim()) && c.AssayGroupSubsampleID == ssGuid);
            foreach (AssayGroupSubsamplePrecondition xx in res)
            {
                agw = xx;
            }
            if (agw == null)
            {
                agw = new AssayGroupSubsamplePrecondition();
                agw.PreconditionName = preconditionName;
                agw.AssayGroupSubsampleID = ssGuid;
                agw.AssayGroupSubsamplePreconditionID = Guid.NewGuid();
                
                entityObj.AssayGroupSubsamplePreconditions.AddObject(agw);
                entityObj.SaveChanges();

            }
            return agw;
        }

        private AssayGroupWorkflow GetAssayGroupWorkflow(XODBC entityObj, string programType, Guid? assayGroupID)
        {
            AssayGroupWorkflow agw = null;
            IQueryable<AssayGroupWorkflow> res = entityObj.AssayGroupWorkflows.Where(c => c.WorkflowName.Trim().Equals(programType.Trim()) && c.AssayGroupID == assayGroupID);
            foreach (AssayGroupWorkflow xx in res)
            {
                agw = xx;
            }
            if (agw == null) {
                agw = new AssayGroupWorkflow();
                agw.AssayGroupID = assayGroupID;
                agw.AssayGroupWorkflowID = Guid.NewGuid();
                agw.WorkflowName = programType;
                entityObj.AssayGroupWorkflows.AddObject(agw);
                entityObj.SaveChanges();

            }
            return agw;
        }

        private AssayGroupTest FindExistingAssayGroupTest(string p)
        {
            using (var entityObj = new XODBC(BaseImportTools.XSTRING, null))
            {
                AssayGroupTest resAssGroup = null;
                IQueryable<AssayGroupTest> res = entityObj.AssayGroupTests.Where(c => c.AssayTestName.Trim().Equals(p.Trim()));
                foreach (AssayGroupTest xx in res)
                {
                    resAssGroup = xx;
                }
                return resAssGroup;
            }

        }

        private Guid FindParameterForAssayTypeName(string pName)
        {
            Guid pid = new Guid();
            Parameter xp = new Parameter();

            using (var entityObj = new XODBC(BaseImportTools.XSTRING, null))
            {
                bool found = false;
                IQueryable<Parameter> res = entityObj.Parameters.Where(c => c.ParameterType.Equals("AssayTypeName") && c.ParameterName.Equals(pName));
                foreach (Parameter xx in res)
                {
                    found = true;
                    pid = xx.ParameterID;
                    break;
                }
                if (!found)
                {
                    Parameter pp = new Parameter();
                    pid = Guid.NewGuid();
                    pp.ParameterID = pid;
                    pp.ParameterType = "AssayTypeName";
                    pp.ParameterName = pName;
                    pp.Description = pName;
                    pp.VersionUpdated = DateTime.UtcNow;
                    entityObj.Parameters.AddObject(pp);
                    entityObj.SaveChanges();
                }

                return pid;
            }
        }

       private static void PopulateCMapShortcut(string lookupString, FormatSpecification.ImportDataMap importMap, Dictionary<string, int> columnIDX)
       {
           ColumnMap cmap = importMap.FindItemsByTargetName(lookupString);
           if (cmap != null)
           {
               columnIDX.Add(lookupString, cmap.sourceColumnNumber);
           }
       }

        /// <summary>
        /// Find the Guid for the given value in the foreign table.  If it does not exist, create it.
        /// </summary>
        /// <param name="columnValue"></param>
        /// <param name="cmap"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
     


        private List<string> parseTestLine(string ln, char delim)
        {
            string[] items = ln.Split(new char[] { delim }, StringSplitOptions.None);
            return new List<string>(items);

        }

    }
}