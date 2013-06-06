using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XODB.Import.Client.DataRecords;

namespace XODB.Import.Client.Processing
{
    class RowProcessing
    {
        
        internal string QueryRow(int rowNum, ColumnManager columnManager)
        {
            string ss = columnManager.GetRowAsStringAt(rowNum);
            return ss;
        }
    }
}
