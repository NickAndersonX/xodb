using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using XODB.Import.FormatSpecification;

namespace XODB.Import.ImportUtils
{
    public class CollarImport
    {

        static bool commitToDB = true;

        internal void AddCollarData(ModelImportStatus mos, Stream fileStream, FormatSpecification.ImportDataMap importMap, int batchSize, Action<string, double> UpdateStatus, int approxNumLines, string connectionString, List<string> existingHoleNames, Guid XODBProjectID, bool overwrite)
        {

            
            bool hasDuplicateHoles = false;
            // iterate through the data lines
            int ct = 1;
            int linesRead = 0;
            SqlConnection connection = null;
            SqlConnection secondaryConnection = null;
            Dictionary<string,int> uniqueHoleNames = new Dictionary<string,int>();
            List<List<string>> rejectedLines = new List<List<string>>();
            Dictionary<string, string> holeWarningMessages = new Dictionary<string, string>(); 
            // get a connection to the database
            try
            {
                
                connection = new SqlConnection(connectionString);
                connection.Open();

                secondaryConnection = new SqlConnection(connectionString);
                secondaryConnection.Open();


                int numCommits = 0;
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
                float bct = 1;

                // report every X blocks
                int repCount = 0;
                //int reportOnBlock = 1000;
                float fNumLines = (float)approxNumLines;
                

                // get the column containing the hole name 
                ColumnMap cmapHeader = importMap.FindItemsByTargetName("HoleName");
                
                int headerIDX = cmapHeader.sourceColumnNumber;
                int numberOfHolesAdded = 0;
                
                if (sr != null)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        repCount++;

                   
                        bct++;
                        linesRead++;
                        if (ct >= importMap.dataStartLine)
                        {
                            
                            
                            string statementPart1 = "INSERT INTO " + importMap.mapTargetPrimaryTable + " ";
                            string clauseValues = "";
                            string clauseParameters = "";

                            List<string> items = parseTestLine(line, importMap.inputDelimiter);
                            // using the column map, pick out the hole name field and see if it is in the database already
                            string headerNameItem = items[headerIDX];
                            // check if this holename is a duplicate in the file

                            bool hasHolenameEntryInFile = uniqueHoleNames.ContainsKey(headerNameItem.Trim());
                            if (hasHolenameEntryInFile) { 
                                int val = uniqueHoleNames[headerNameItem.Trim()];
                                uniqueHoleNames[headerNameItem.Trim()] = val++;
                                holeWarningMessages.Add(headerNameItem+" ("+val+")", "Hole: " + headerNameItem + " at line " + linesRead + " already exists in the input file - skipping.");
                                mos.recordsFailed++;
                                continue;
                            } else {
                                uniqueHoleNames.Add(headerNameItem.Trim(), 1);
                            }

                            // check if this hole exists in the database
                            if (existingHoleNames.Contains(headerNameItem.Trim()))
                            {
                                if (!holeWarningMessages.ContainsKey(headerNameItem))
                                {
                                    holeWarningMessages.Add(headerNameItem, "Hole: " + headerNameItem + " already exists in this project. Skipping record at line " + linesRead + ".");
                                    if(!overwrite){
                                        mos.recordsFailed ++;
                                    }
                                }
                                else { 
                                    
                                }
                                
                                mos.finalErrorCode = ModelImportStatus.DATA_CONSISTENCY_ERROR;
                                hasDuplicateHoles = true;
                                
                                rejectedLines.Add(items);
                                continue;
                            }



                            #region mappsearch
                            // now pick out all the mapped values
                            foreach (ColumnMap cmap in importMap.columnMap)
                            {
                                bool isFKColumn = cmap.hasFKRelation;

                                int colID = cmap.sourceColumnNumber;
                                string columnValue = cmap.defaultValue;
                                if (colID >= 0)
                                {
                                    columnValue = items[colID];
                                }
                         
                                string targetCol = cmap.targetColumnName;
                                string targetTable = cmap.targetColumnTable;
                                clauseValues += "" + targetTable + "." + targetCol + ",";


                                if (isFKColumn) { 
                                    // go and search for the appropriate value from the foreign key table
                                    string newValue = ForeignKeyUtils.FindFKValueInDictionary(columnValue, cmap, secondaryConnection, true);
                                    columnValue = newValue;
                                }

                                if (cmap.importDataType.Equals(ImportDataMap.NUMERICDATATYPE))
                                {
                                    if (columnValue.Equals("-"))
                                    {
                                        if (cmap.defaultValue != null && cmap.defaultValue.Length > 0)
                                        {
                                            columnValue = cmap.defaultValue;
                                            
                                        }
                                        else
                                        {
                                            columnValue = "NULL";
                                        }
                                    }
                                    
                                    clauseParameters += columnValue + ",";
                                }else if (cmap.importDataType.Equals(ImportDataMap.TIMESTAMPDATATYPE))
                                {
                                    DateTime dtr = new DateTime();
                                    bool parsed = DateTime.TryParse(columnValue, out dtr);
                                    if (parsed)
                                    {
                                        columnValue = "\'" + dtr.ToString("yyyy-MM-dd hh:mm:ss tt") + "\'";
                                        
                                    }
                                    else {
                                        columnValue = "NULL";
                                    }
                                    clauseParameters += columnValue + ",";
                                }

                                else
                                {
                                    //if (columnValue.Equals("-"))
                                    //{
                                    //    if (cmap.defaultValue != null && cmap.defaultValue.Length > 0)
                                    //    {
                                    //        columnValue = cmap.defaultValue;
                                    //    }

                                    //}
                                    clauseParameters += "\'" + columnValue + "\',";
                                }
                            }
                            #endregion
                            // now just a hack to remove the final coma from the query
                            clauseParameters = clauseParameters.Substring(0, clauseParameters.Length - 1);
                            clauseValues = clauseValues.Substring(0, clauseValues.Length - 1);

                            string commandText = statementPart1 + "(" + clauseValues + ") VALUES (" + clauseParameters + ")";
                            SqlCommand sqc = new SqlCommand(commandText, connection, trans);
                            numberOfHolesAdded++;
                            if (commitToDB)
                            {
                                sqc.ExecuteNonQuery();
                            }
                            tb++;
                            if (tb == transactionBatchLimit)
                            {
                                // commit batch, then renew the transaction
                                if (commitToDB)
                                {
                                    trans.Commit();
                                    numCommits++;
                                    //   trans = null;
                                    trans = connection.BeginTransaction();
                                }
                                // reset counter
                                tb = 0;
                            }
                        }
                        ct++;
                    }
                }
                if (tb > 0)
                {
                    if (commitToDB)
                    {
                        trans.Commit();
                    }
                    numCommits++;
                }
                mos.recordsAdded = numberOfHolesAdded;
                UpdateStatus("Finished writing collars to database ", 100.0);
            }
            catch (Exception ex)
            {
                UpdateStatus("Error writing collars to database ", 0);
                mos.AddErrorMessage("Error writing collar data at line " + linesRead + ":\n" + ex.ToString());
                mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
            }
            finally
            {
                try { 
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


            if (hasDuplicateHoles == true && overwrite == true)
            {
                OverwriteCollarRecord(mos, rejectedLines , importMap, connectionString, XODBProjectID, UpdateStatus, holeWarningMessages);
            }
            foreach (KeyValuePair<string, string> kvp in holeWarningMessages) {
                string v =  kvp.Value;
                mos.AddWarningMessage(v);
            }

            mos.linesReadFromSource = linesRead;
           
         
        }

        private void OverwriteCollarRecord(ModelImportStatus mos, List<List<string>> rejectedLines, ImportDataMap importMap, string connectionString, Guid XODBProjectID, Action<string, double> UpdateStatus, Dictionary<string, string> holeWarningMessages)
        {
            SqlConnection connection = null;
            SqlConnection secondaryConnection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                secondaryConnection = new SqlConnection(connectionString);
                secondaryConnection.Open();
                int numCommits = 0;
                SqlTransaction trans;
                trans = connection.BeginTransaction();
                List<SqlCommand> commands = new List<SqlCommand>();
                int tb = 0;
                int transactionBatchLimit = 10;
                // open the filestream and read the first line              
                float bct = 1;
                // report every X blocks
                int repCount = 0;
                //int reportOnBlock = 1000;
                float fNumLines = (float)rejectedLines.Count();
                

                // get the column containing the hole name 
                ColumnMap cmapHeader = importMap.FindItemsByTargetName("HoleName");
                
                int headerIDX = cmapHeader.sourceColumnNumber;
                int numberOfHolesAdded = 0;
                int linesRead = 0;
                int ct = 1;

                // get all fo the header IDs in one go before we try the insert
                
                Dictionary<string, Guid> holeIDLookups = CollarQueries.FindHeaderGuidsForProject(XODBProjectID);
               


                foreach (List<string> columnData in rejectedLines)
                {
                    
                        linesRead++;
                        repCount++;


                        bct++;

                        string statementPart1 = "UPDATE " + importMap.mapTargetPrimaryTable + " ";
                        string clauseValues = "";


                        // using the column map, pick out the hole name field and see if it is in the database already
                        string headerNameItem = columnData[headerIDX];
                        string headerGUID = "";
                        bool lv = holeIDLookups.ContainsKey(headerNameItem);
                        if (!lv)
                        {
                           // oops - no hole ID with this name - should not happen though!!
                        }
                        else
                        {
                            Guid holeGuid = new Guid();
                            holeIDLookups.TryGetValue(headerNameItem, out holeGuid);
                            headerGUID = holeGuid.ToString();
                        }

                        #region mappsearch
                        // now pick out all the mapped values
                        foreach (ColumnMap cmap in importMap.columnMap)
                        {
                            bool isFKColumn = cmap.hasFKRelation;
                            int colID = cmap.sourceColumnNumber;
                            string columnValue = cmap.defaultValue;
                            if (colID >= 0)
                            {
                                columnValue = columnData[colID];
                            }

                            string targetCol = cmap.targetColumnName;
                            // ignore mapped hole name and project ID columns
                            if (targetCol.Trim().Equals("HoleName") || targetCol.Trim().Equals("ProjectID"))
                            {
                                continue;
                            }
                            string targetTable = cmap.targetColumnTable;

                            clauseValues += "" + targetTable + "." + targetCol + "=";


                            if (isFKColumn)
                            {
                                // go and search for the appropriate value from the foreign key table
                                string newValue = ForeignKeyUtils.FindFKValueInDictionary(columnValue, cmap, secondaryConnection, true);
                                columnValue = newValue;
                            }

                            if (cmap.importDataType.Equals(ImportDataMap.NUMERICDATATYPE))
                            {
                                if (columnValue.Equals("-"))
                                {
                                    if (cmap.defaultValue != null && cmap.defaultValue.Length > 0)
                                    {
                                        columnValue = cmap.defaultValue;
                                    }
                                    else
                                    {
                                        columnValue = "NULL";
                                    }
                                }

                            }
                            else if (cmap.importDataType.Equals(ImportDataMap.TIMESTAMPDATATYPE))
                            {
                                DateTime dtr = new DateTime();
                                bool parsed = DateTime.TryParse(columnValue, out dtr);
                                if (parsed)
                                {
                                    columnValue = "\'" + dtr.ToString("yyyy-MM-dd hh:mm:ss tt") + "\'";

                                }
                                else
                                {
                                    columnValue = "NULL";
                                }
                              
                            }
                            else
                            {
                                columnValue += "\'" + columnValue + "\'";
                            }
                            clauseValues += columnValue + ",";

                        }
                        #endregion
                        // now just a hack to remove the final coma from the query
                        clauseValues = clauseValues.Substring(0, clauseValues.Length - 1);

                        string commandText = statementPart1 + "SET " + clauseValues + " WHERE HeaderID=\'" + headerGUID + "\' AND ProjectID=\'" + XODBProjectID.ToString() + "\';";
                        SqlCommand sqc = new SqlCommand(commandText, connection, trans);
                        string msg = "";    
                        //holeWarningMessages.TryGetValue(headerNameItem, out msg);
                        msg = "Hole "+headerNameItem + " was overwritten with new data";
                        holeWarningMessages[headerNameItem] = msg;

                        numberOfHolesAdded++;
                        if (commitToDB)
                        {
                            sqc.ExecuteNonQuery();
                        }
                        tb++;
                        if (tb == transactionBatchLimit)
                        {
                            // commit batch, then renew the transaction
                            if (commitToDB)
                            {
                                trans.Commit();
                                numCommits++;
                                //   trans = null;
                                trans = connection.BeginTransaction();
                            }
                            // reset counter
                            tb = 0;
                        }

                        ct++;
                    }

                    if (tb > 0)
                    {
                        if (commitToDB)
                        {
                            trans.Commit();
                        }
                        numCommits++;
                    }
                    mos.recordsUpdated = numberOfHolesAdded;
                    UpdateStatus("Finished writing collars to database ", 100.0);
                }
            
            catch (Exception ex)
            {
                UpdateStatus("Error writing collars to database ", 0);
                mos.AddErrorMessage("Error writing collar data at line " + rejectedLines.Count + ":\n" + ex.ToString());
                mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
            }
            finally
            {
                try
                {
                    connection.Close();
                    secondaryConnection.Close();

                    
                }
                catch (Exception ex)
                {
                    mos.AddErrorMessage("Error closing conenction to database:\n" + ex.ToString());
                    mos.finalErrorCode = ModelImportStatus.ERROR_WRITING_TO_DB;
                }
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
