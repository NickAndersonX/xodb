using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XODB.Import
{
    /// <summary>
    /// Class to store data about the import progress
    /// </summary>
    public class ModelImportStatus
    {
        public Guid modelID { get; set; }

        List<string> errorMessages = new List<string>();
        List<string> warningMessages = new List<string>();
        public int finalErrorCode {get; set;}
        public static int OK = 0; 
        public static int ERROR_LOADING_FILE = 1;
        public static int ERROR_LOADING_DEFINITION_FILE = 2;
        public static int ERROR_CONNECTING_TO_DB = 3;
        public static int ERROR_WRITING_TO_DB = 4;

        private int _recordsImported = 0;
        public int RecordsImported { get { return _recordsImported; } set { _recordsImported = value; } }

        public ModelImportStatus()
        {
            modelID = new Guid();
        }
         public ModelImportStatus(Guid _id) {
             modelID = _id;
         }

            
     
        public void AddErrorMessage(string str){
            errorMessages.Add(str);
        }



        public void AddWarningMessage(string p)
        {
            warningMessages.Add(p);
        }

        public int linesReadFromSource { get; set; }

        //All possible lines //TODO: read all lines from source 
        public int TotalLines { get; set; }

        public string importTextFileName { get; set; }

        public string targetModelName { get; set; }

        
        /// <summary>
        /// generate a human readable message 
        /// </summary>
        /// <returns></returns>
        public string GenerateStringMessage()
        {
            string res = "";
            if (finalErrorCode == 0)
            {
                res += "Model imported into XODB";
            }
            if (finalErrorCode == 1) {
                res += "Error loading data file";
            }
            else if (finalErrorCode == 2)
            {
                res += "Error loading definition file";
            }else if (finalErrorCode == 3)
            {
                res += "Error commuinicating with XODB database";
            }
            else if (finalErrorCode == 3)
            {
                res += "Error writing blocks to XODB database";
            }
            int ct = 1;
            if (errorMessages.Count > 0)
            {
                res += "\n\n" + errorMessages.Count + " error messages during block model import";
                foreach (string m in errorMessages)
                {
                    res += ct + ") " + m;
                    res += "---------------------------------------------------";
                    ct++;
                }
            }

            if (warningMessages.Count >0)
            {
                res += "\n\n"+warningMessages.Count + " warning messages during block model import";
                ct = 1;
                foreach (string m in warningMessages)
                {
                    res += ct + ") " + m;
                    res += "---------------------------------------------------";
                    ct++;
                }
            }

            res += string.Format("\n\n For File: {0}", importTextFileName);

            res += string.Format("\n\n For Model: {0}", targetModelName);

            res += string.Format("\n\n Lines Read (including headers): {0}", linesReadFromSource);

            res += string.Format("\n\n Rows Imported: {0}", _recordsImported);
            return res;


        }
    }
}
