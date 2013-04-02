using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XODB.Import.FormatSpecification
{
    public class ImportDataMap
    {
        public static string NUMERICDATATYPE = "NUMERIC";
        public static string TEXTDATATYPE = "TEXT";
        public static string UNIT_NONE = null;
        public static string UNIT_PPM = "PPM";
        public static string UNIT_PCT = "PCT";

        public ImportDataMap()
        {
            columnMap = new List<ColumnMap>();
        }

        public string mapName { get; set; }                 // name to give the map
        public DateTime mapDate { get; set; }               // date that this map was created
        public string mapOriginalDataFile { get; set; }     // name of the original file for reference
        public string mapDescription { get; set; }          // description 
        public string mapMainDataType { get; set; }         // e.g. Collar, Block model, Assay etc.

        public string mapTargetPrimaryTable { get; set; }   // e.g. The main table mapped to in the database

        public char inputDelimiter { get; set; }            // e.g. comma, tab, space etc.
        public int dataStartLine { get; set; }              // the line the data starts at
        public List<ColumnMap> columnMap { get; set; }        // List contining all the maps pertaining to this particular import definition




        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="XODBColumnName"></param>
        /// <returns></returns>
        public int GetColumnIDMappedTo(string XODBColumnName)
        {
            int res = -1;

            foreach (ColumnMap cm in columnMap)
            {
                if (cm.targetColumnName.Trim().Equals(XODBColumnName.Trim()))
                {
                    res = cm.sourceColumnNumber;
                    break;
                }
            }
            return res;
        }

    }
}
