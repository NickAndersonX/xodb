namespace XODB.Reports
{
    partial class CompareModelReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart2});
            this.Detail.HeightF = 518.3333F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrChart2
            // 
            this.xrChart2.BorderColor = System.Drawing.Color.Black;
            this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart2.DataMember = "gt";
            xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisX.Title.Text = "Grade";
            xyDiagram1.AxisX.Title.Visible = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisY.Title.Text = "Volume";
            xyDiagram1.AxisY.Title.Visible = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
            this.xrChart2.Diagram = xyDiagram1;
            this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrChart2.Name = "xrChart2";
            this.xrChart2.PaletteName = "Aspect";
            series1.ArgumentDataMember = "grade";
            series1.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
            new DevExpress.XtraCharts.DataFilter("m", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, "1")});
            pointSeriesLabel1.LineVisible = true;
            series1.Label = pointSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "Model 1";
            series1.ValueDataMembersSerializable = "tonnage";
            splineSeriesView1.LineMarkerOptions.Visible = false;
            series1.View = splineSeriesView1;
            series2.ArgumentDataMember = "grade";
            series2.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
            new DevExpress.XtraCharts.DataFilter("m", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, "2")});
            pointSeriesLabel2.LineVisible = true;
            series2.Label = pointSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Name = "Model 2";
            series2.ValueDataMembersSerializable = "tonnage";
            splineSeriesView2.LineMarkerOptions.Visible = false;
            series2.View = splineSeriesView2;
            this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            pointSeriesLabel3.LineVisible = true;
            this.xrChart2.SeriesTemplate.Label = pointSeriesLabel3;
            this.xrChart2.SeriesTemplate.View = splineSeriesView3;
            this.xrChart2.SizeF = new System.Drawing.SizeF(650F, 479.5833F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 65.625F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.DetailReport});
            this.DetailReport1.DataMember = "gfs";
            this.DetailReport1.Level = 0;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail2.HeightF = 40.625F;
            this.Detail2.Name = "Detail2";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(650F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Arial", 7F);
            this.xrTableCell2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseForeColor = false;
            this.xrTableCell2.Text = "[FilterString]";
            this.xrTableCell2.Weight = 3D;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataMember = "gfc";
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail1.Name = "Detail1";
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.Color.LightGray;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(10.00002F, 30.20833F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(630.0001F, 50F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.BackColor = System.Drawing.Color.Black;
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.ForeColor = System.Drawing.Color.White;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBackColor = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseForeColor = false;
            this.xrTableCell8.Text = "Resource";
            this.xrTableCell8.Weight = 0.34807684027867347D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BackColor = System.Drawing.Color.Black;
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.ForeColor = System.Drawing.Color.White;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBackColor = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseForeColor = false;
            this.xrTableCell9.Text = "Model 1 Tonnes";
            this.xrTableCell9.Weight = 0.55288454478486215D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BackColor = System.Drawing.Color.Black;
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.ForeColor = System.Drawing.Color.White;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBackColor = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseForeColor = false;
            this.xrTableCell10.Text = "Model 1 Grade";
            this.xrTableCell10.Weight = 0.51442321607521779D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BackColor = System.Drawing.Color.Black;
            this.xrTableCell11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.ForeColor = System.Drawing.Color.White;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseForeColor = false;
            this.xrTableCell11.Text = "Model 2 Tonnes";
            this.xrTableCell11.Weight = 0.5057690832791415D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BackColor = System.Drawing.Color.Black;
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.ForeColor = System.Drawing.Color.White;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBackColor = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseForeColor = false;
            this.xrTableCell12.Text = "Model 2 Grade";
            this.xrTableCell12.Weight = 0.49807667961208096D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BackColor = System.Drawing.Color.Black;
            this.xrTableCell13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.ForeColor = System.Drawing.Color.White;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBackColor = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseForeColor = false;
            this.xrTableCell13.Text = "Difference";
            this.xrTableCell13.Weight = 0.48846188300526511D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "[Resource Category]";
            this.xrTableCell1.Weight = 0.34807684027867347D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "[Model 1 Tonnes]";
            this.xrTableCell3.Weight = 0.55288454478486215D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "[Model 1 Grade]";
            this.xrTableCell4.Weight = 0.51442321607521779D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "[Model 2 Tonnes]";
            this.xrTableCell5.Weight = 0.5057690832791415D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "[Model 2 Grade]";
            this.xrTableCell6.Weight = 0.49807667961208096D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "[Absolute Difference]";
            this.xrTableCell7.Weight = 0.48846188300526511D;
            // 
            // CompareModelReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport1});
            this.DataMember = "OK";
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 66, 100);
            this.Version = "12.1";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRChart xrChart2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
    }
}
