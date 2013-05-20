using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xstract.Import.LAS;

namespace XODB.Import.Client.Processing
{
    public class LASImport
    {

        public LASImport() { }

        public LASFile ImportLASFile(string inputFilename, string outputCSV) {
            LASFile lf = null;
            try
            {
                LASFileReader lfr = new LASFileReader();
                int columnOffset = 0;
                int errorCode = 0;
                 lf = lfr.ReadLASFile(inputFilename, columnOffset, out errorCode);
                
                if (errorCode == 0)
                {
                    string res = "";
                    foreach (string nc in lf.columnHeaders)
                    {
                        res += nc + ", ";
                    }

  
                    // Display error mesasges if required
                    if (lf.errorDetails != null && lf.errorDetails.Count > 0)
                    {
                        string messageBoxText = "The file selected was loaded, but issues were noted as follows:";

                        foreach (string ed in lf.errorDetails)
                        {
                            messageBoxText += "\n" + ed;
                        }

                       

                    }

                }
                else
                {

                    string messageBoxText = "The file selected could not be loaded.  Please check that the selected file is " +
                                            "accessible, is not open in another application and is in the correct format.";
                    string errorCodeDetails = LASErrorCodes.LookupCode(errorCode);
                    if (lf != null && lf.errorDetails != null)
                    {
                        foreach (string ed in lf.errorDetails)
                        {
                            messageBoxText += "\n" + ed;
                        }
                    }
                    string caption = "Error loading file";
                    
                }

            }
            catch (Exception ex)
            {

            }
            return lf;
        }
    }
}
