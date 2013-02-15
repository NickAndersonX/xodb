using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.Web.Mvc;
using System.Web.Mvc;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.XtraReports.Parameters;
using XODB.Helpers;
using System.Data;
using XODB.ViewModels;
using XODB.Services;


namespace XODB.Reports
{
    public partial class CompareModelReport : ReportHelper.TableReport
    {
        public CompareModelReport()
        {
            InitializeComponent();
        }


        public CompareModelReport(ReportHelper.ITableReportDataFiller filler)
            : base(filler)
        {
            InitializeComponent();
            //Name = ReportNames.TableReport;
            //DisplayName = ReportNames.TableReport;
        }

        public class DataProvider : ReportHelper.ITableReportDataFiller
        {

            private IReport _r { get; set; }

            public DataProvider()
                : base()
            { }

            public DataProvider(IReport r)
                : base()
            {
                _r = r;
            }

            public void Fill(ReportHelper.TableReport report)
            {
                var o = (BlockModelCompareViewModel)_r;
                string filterString;
                report.DataSource = BlockModelService.CompareModelsResult(o);
            }
        }
    }
}
