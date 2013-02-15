using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.XtraReports.Parameters;
using XODB.Helpers;
using XODB.ViewModels;
using ImpromptuInterface;
using XODB.Reports;


namespace XODB.Reports
{
    public static class AllReports
    {
        public enum ReportType : uint
        {
             CompareModel = 0
        }

        static readonly Dictionary<ReportType, ReportHelper.ReportRegistrationItem> reports = new Dictionary<ReportType, ReportHelper.ReportRegistrationItem> {
            {ReportType.CompareModel, new ReportHelper.ReportRegistrationItem() {
                ReportBuilder = (r) => new CompareModelReport(new CompareModelReport.DataProvider(r)) { DataAdapter = null },
                ParametersView = "CompareModelParametersPartial"
                }
            }
        };

        public static IReport CreateModel(ReportType r)
        {
            return new 
            {
                ReportID = (uint)r,
                ReportName = System.Enum.GetName(typeof(ReportType), r),
                Report = GetReport(r),
                ParametersView = GetParametersViewName(r)
            }.ActLike<IReport>();
        }

        public static IReport CreateModel(IReport r)
        {
            return new
            {
                ReportID = (uint)r.ReportID,
                ReportName = System.Enum.GetName(typeof(ReportType), r.ReportID),
                Report = GetReport(r),
                ParametersView = GetParametersViewName((ReportType)r.ReportID)
            }.ActLike<IReport>();
        }

        public static IReport CreateModel(ReportType r, Dictionary<string, string> parameter)
        {
            var model = CreateModel(r);
            if (parameter != null)
            {
                foreach (var key in parameter.Keys)
                {
                    DevExpress.XtraReports.Parameters.Parameter param = model.Report.Parameters[key];
                    if (param == null)
                        continue;
                    param.Value = ObjectHelper.ConvertType(parameter[key], param.Type);
                }
            }
            return model;
        }
        
        public static XtraReport GetReport(ReportType r)
        {
            return reports[r].ReportBuilder(null);
        }

        public static XtraReport GetReport(IReport r)
        {
            return reports[(ReportType)r.ReportID].ReportBuilder(r);
        }
        public static string GetParametersViewName(ReportType r)
        {
            return reports[r].ParametersView;
        }

    }
}