using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileHandler.IO
{
    public class IOResults
    {
        public string errorMessage;
        public int errorCondition { get; set; }

        public string extendedErrorMessage { get; set; }
    }
}
