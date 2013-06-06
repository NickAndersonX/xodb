using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xstract.Import.LAS
{
   public  class LASFile
    {
        public List<LASDataRow> dataRows;
        public List<string> columnHeaders;



        public string filename { get; set; }

        public double nullValue = -99999.0;
        public double versionValue = 2.0;

        public List<string> errorDetails { get; set; }
    }
}
