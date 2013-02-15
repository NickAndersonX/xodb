using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;

namespace XODB.ViewModels
{
    public interface IReport
    {
        string ReportName { get; set; }
        uint ReportID { get; set; }
        XtraReport Report { get; set; }
        string ParametersView { get; set; }
        string SerializedChild { get; set; }
        string FilterString { get; set; }
    }
}