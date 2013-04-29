using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using XODB.Helpers;
using DevExpress.XtraReports.UI;
using System;

namespace XODB.ViewModels
{
    public class AssayReportViewModel : IReport
    {
        [DisplayName("Project Name:")]
        public string Project { get; set; }
        [Required, DisplayName("Project ID:")]
        public Guid ProjectID { get; set; }

        [DisplayName("Report ID:")]
        public uint ReportID { get; set; }
        [DisplayName("Report Name:")]
        public string ReportName { get; set; }
        [DisplayName("Report:")]
        public XtraReport Report { get; set; }
        [DisplayName("Parameters:")]
        public string ParametersView { get; set; }
        [DisplayName("Report Executed By Username:")]
        public string ReportExecutedByUserName { get; set; }
        [DisplayName("Report Executed By User:")]
        public Guid ReportExecutedByUserID { get; set; }
        [DisplayName("Serialized Info:")]
        public string SerializedChild { get; set; }
        [DisplayName("Filter String:")]
        public string FilterString { get; set; }

        public SelectList Projects { get; set; }

    }
}