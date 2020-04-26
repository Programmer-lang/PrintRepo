using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using ReportDesignerSample.Views;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace ReportDesignerSample.ViewModels
{
    [POCOViewModel]
    public class ViewModel1
    {

        //public void Print()

        //{

        //    try
        //    {

        //        XtraReport report = new XtraReport()
        //        {
        //            Name = "PrintMyData",
        //            DisplayName = "Recent Apps",
        //            PaperKind = PaperKind.Letter,
        //            Margins = new Margins(100, 100, 100, 100)
        //        };


        //        BindToData(report);
        //        CreateReportHeader(report, "Print Data");
        //        CreateDetail(report);
        //        CreateDetailReport(report);

        //        MyReport MyReport = new MyReport();
        //        MyReport.ReportDesigner.OpenDocument(report);
        //        MyReport.Show();

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.InnerException.Message);

        //    }
        //}

        public void Print()

        {

            try
            {

                XtraReport report = new XtraReport()
                {
                    Name = "PrintMyData",
                    DisplayName = "Recent Apps",
                    PaperKind = PaperKind.Letter,
                    Margins = new Margins(100, 100, 100, 100)
                };


                BindToData(report);
                CreateReportHeader(report, "Print Data");
                CreateDetail(report);
                CreateDetailReport(report);

                MyReport MyReport = new MyReport();
                MyReport.ReportDesigner.OpenDocument(report);
                MyReport.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);

            }
        }

        // SQL
        //public void BindToData(XtraReport report)
        //{
        //    Model1 MyContext = new Model1();

        //    string ConnectionString = MyContext.Database.Connection.ConnectionString;
        //    CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(ConnectionString);

        //    DevExpress.DataAccess.Sql.SqlDataSource ds = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);

        //    CustomSqlQuery QueryMaster = new CustomSqlQuery();
        //    QueryMaster.Name = "Employes";
        //    QueryMaster.Sql = "SELECT * FROM Employes";

        //    CustomSqlQuery QueryChild = new CustomSqlQuery();
        //    QueryChild.Name = "Orders";
        //    QueryChild.Sql = "SELECT * FROM Orders";

        //    ds.Queries.AddRange(new SqlQuery[] { QueryMaster, QueryChild });
        //    ds.Relations.Add(QueryMaster.Name, QueryChild.Name, "Id", "EmployeId");

        //    report.DataSource = ds;
        //    report.DataMember = QueryMaster.Name;
        //}


        public ObservableCollection<Employe> MyList { get; set; }


        // Collection
        public void BindToData(XtraReport report)
        {
            Model1 MyContext = new Model1();

            var MyList1 = MyContext.Employes.ToList();
           
            MyList = new ObservableCollection<Employe>(MyList1);

            string ConnectionString = MyContext.Database.Connection.ConnectionString;
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(ConnectionString);

            report.DataSource = MyList;
            
        }

        public void CreateReportHeader(XtraReport report, string caption)
        {

            XRLabel Label = new XRLabel();
            Label.Font = new Font("Tahoma", 12, System.Drawing.FontStyle.Bold);
            Label.Text = caption;
            Label.WidthF = 300F;

            ReportHeaderBand ReportHeader = new ReportHeaderBand();
            report.Bands.Add(ReportHeader);
            ReportHeader.Controls.Add(Label);
            ReportHeader.HeightF = Label.HeightF;
        }

        public void CreateDetail(XtraReport report)
        {

            // First Name
            XRLabel labelDetail = new XRLabel();
            SetLabelProperties(labelDetail, "First Name", "FirstName");

            // Last Name
            XRLabel labelDetail2 = new XRLabel();
            SetLabelProperties(labelDetail2, "Last Name", "LastName");

            DetailBand detailBand = new DetailBand();
            detailBand.Height = labelDetail.Height;
            detailBand.KeepTogetherWithDetailReports = true;
            report.Bands.Add(detailBand);

            labelDetail.TopF = detailBand.LocationFloat.Y + 20F;
            labelDetail2.TopF = detailBand.LocationFloat.Y + 20F;
            labelDetail2.LeftF = detailBand.LocationFloat.X + 200F;

            detailBand.BorderDashStyle = BorderDashStyle.Solid;
            detailBand.BorderColor = Color.Black;
            detailBand.Controls.Add(labelDetail);
            detailBand.Controls.Add(labelDetail2);


        }

        public void SetLabelProperties(XRLabel label, String title, String member)
        {

            label.Font = new Font("Tahoma", 10, System.Drawing.FontStyle.Regular);
            label.WidthF = 200F;
            label.Borders = BorderSide.All;
            label.BackColor = Color.LightBlue;
            label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            label.ExpressionBindings.Add(
            new ExpressionBinding("BeforePrint", "Text", "'" + title + ": ' + [" + member + "]"));

        }

        // sql
        //public void CreateDetailReport(XtraReport report)
        //{
        //    string DataMember;

        //    SqlDataSource ds = report.DataSource as SqlDataSource;
        //    DataMember = ds.Queries[0].Name + "." + ds.Relations[0].Name;

        //    DetailReportBand detailReportBand = new DetailReportBand();
        //    report.Bands.Add(detailReportBand);
        //    detailReportBand.DataSource = report.DataSource;
        //    detailReportBand.DataMember = DataMember;
          
        //    // Add a header to the detail report.
        //    ReportHeaderBand detailReportHeader = new ReportHeaderBand();
        //    detailReportBand.Bands.Add(detailReportHeader);

        //    XRTable tableHeader = new XRTable();
        //    tableHeader.BeginInit();
        //    tableHeader.Rows.Add(new XRTableRow());
        //    tableHeader.Borders = BorderSide.All;
        //    tableHeader.BorderColor = Color.DarkGray;
        //    tableHeader.Font = new Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
        //    tableHeader.Padding = 10;
        //    tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

        //    XRTableCell cellHeader1 = new XRTableCell();
        //    cellHeader1.Text = "Order No";
        //    cellHeader1.BackColor = Color.LightGray;

        //    XRTableCell cellHeader2 = new XRTableCell();
        //    cellHeader2.Text = "Description";
        //    cellHeader2.BackColor = Color.LightGray;

        //    tableHeader.Rows[0].Cells.AddRange(new XRTableCell[] { cellHeader1 , cellHeader2 });
        //    detailReportHeader.HeightF = tableHeader.HeightF;
        //    detailReportHeader.Controls.Add(tableHeader);

        //    // Adjust the table width.
        // //   tableHeader.BeforePrint += tableHeader_BeforePrint;
        //    tableHeader.EndInit();

        //    // Create a detail band.
        //    XRTable tableDetail = new XRTable();
        //    tableDetail.BeginInit();
        //    tableDetail.Rows.Add(new XRTableRow());
        //    tableDetail.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
        //    tableDetail.BorderColor = Color.DarkGray;
        //    tableDetail.Font = new Font("Tahoma", 10);
        //    tableDetail.Padding = 10;
        //    tableDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

        //    XRTableCell cellDetail1 = new XRTableCell();
        //    XRTableCell cellDetail2 = new XRTableCell();

        //    cellDetail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        //    cellDetail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

        //    cellDetail1.ExpressionBindings.Add(new ExpressionBinding("Text", "[Id]"));
        //    cellDetail2.ExpressionBindings.Add(new ExpressionBinding("Text", "[Description]"));

        //    tableDetail.Rows[0].Cells.AddRange(new XRTableCell[] { cellDetail1, cellDetail2 });

        //    DetailBand detailBand = new DetailBand();
        //    detailBand.Height = tableDetail.Height;
        //    detailReportBand.Bands.Add(detailBand);
        //    detailBand.Controls.Add(tableDetail);

        //    XRControlStyle oddStyle = new XRControlStyle();
        //    XRControlStyle evenStyle = new XRControlStyle();

        //    oddStyle.BackColor = Color.WhiteSmoke;
        //    oddStyle.StyleUsing.UseBackColor = true;
        //    oddStyle.Name = "OddStyle";

        //    evenStyle.BackColor = Color.White;
        //    evenStyle.StyleUsing.UseBackColor = true;
        //    evenStyle.Name = "EvenStyle";

        //    report.StyleSheet.AddRange(new XRControlStyle[] { oddStyle, evenStyle });
        //    tableDetail.OddStyleName = oddStyle.Name;
        //    tableDetail.EvenStyleName = evenStyle.Name;
        //}

         
    
        
        // Collection
        public void CreateDetailReport(XtraReport report)
        {

            SqlDataSource ds = report.DataSource as SqlDataSource;
          
            DetailReportBand detailReportBand = new DetailReportBand();
            report.Bands.Add(detailReportBand);
            detailReportBand.DataSource = report.DataSource;
           detailReportBand.DataMember = "Orders";
          

            // Add a header to the detail report.
            ReportHeaderBand detailReportHeader = new ReportHeaderBand();
            detailReportBand.Bands.Add(detailReportHeader);

            XRTable tableHeader = new XRTable();
            tableHeader.BeginInit();
            tableHeader.Rows.Add(new XRTableRow());
            tableHeader.Borders = BorderSide.All;
            tableHeader.BorderColor = Color.DarkGray;
            tableHeader.Font = new Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            tableHeader.Padding = 10;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            XRTableCell cellHeader1 = new XRTableCell();
            cellHeader1.Text = "Order No";
            cellHeader1.BackColor = Color.LightGray;

            XRTableCell cellHeader2 = new XRTableCell();
            cellHeader2.Text = "Description";
            cellHeader2.BackColor = Color.LightGray;

            tableHeader.Rows[0].Cells.AddRange(new XRTableCell[] { cellHeader1, cellHeader2 });
            detailReportHeader.HeightF = tableHeader.HeightF;
            detailReportHeader.Controls.Add(tableHeader);

            // Adjust the table width.
            //   tableHeader.BeforePrint += tableHeader_BeforePrint;
            tableHeader.EndInit();

            // Create a detail band.
            XRTable tableDetail = new XRTable();
            tableDetail.BeginInit();
            tableDetail.Rows.Add(new XRTableRow());
            tableDetail.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            tableDetail.BorderColor = Color.DarkGray;
            tableDetail.Font = new Font("Tahoma", 10);
            tableDetail.Padding = 10;
            tableDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            XRTableCell cellDetail1 = new XRTableCell();
            XRTableCell cellDetail2 = new XRTableCell();

            cellDetail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cellDetail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            cellDetail1.ExpressionBindings.Add(new ExpressionBinding("Text", "[Id]"));
            cellDetail2.ExpressionBindings.Add(new ExpressionBinding("Text", "[Description]"));

            tableDetail.Rows[0].Cells.AddRange(new XRTableCell[] { cellDetail1, cellDetail2 });

            DetailBand detailBand = new DetailBand();
            detailBand.Height = tableDetail.Height;
            detailReportBand.Bands.Add(detailBand);
            detailBand.Controls.Add(tableDetail);

            XRControlStyle oddStyle = new XRControlStyle();
            XRControlStyle evenStyle = new XRControlStyle();

            oddStyle.BackColor = Color.WhiteSmoke;
            oddStyle.StyleUsing.UseBackColor = true;
            oddStyle.Name = "OddStyle";

            evenStyle.BackColor = Color.White;
            evenStyle.StyleUsing.UseBackColor = true;
            evenStyle.Name = "EvenStyle";

            report.StyleSheet.AddRange(new XRControlStyle[] { oddStyle, evenStyle });
            tableDetail.OddStyleName = oddStyle.Name;
            tableDetail.EvenStyleName = evenStyle.Name;
        }


        void tableHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            AdjustTableWidth(sender as XRTable);
        }
        private void AdjustTableWidth(XRTable table)
        {
            XtraReport report = table.RootReport;
            table.WidthF = report.PageWidth - report.Margins.Left - report.Margins.Right;
        }
    }
}