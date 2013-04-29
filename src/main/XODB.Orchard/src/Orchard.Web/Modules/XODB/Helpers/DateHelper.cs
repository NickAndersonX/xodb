using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XODB.Helpers
{
    public static class DateHelper
    {
        public const string DefaultDateFormat = "[yyyyMMdd-hhmmss]";
        public static string OnlineDateFormat { get { return DefaultDateFormat; } }
        public static string NowInOnlineFormat { get { return DateTime.Now.ToString(OnlineDateFormat); } }
    }
}