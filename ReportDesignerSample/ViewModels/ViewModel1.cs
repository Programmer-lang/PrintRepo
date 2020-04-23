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

namespace ReportDesignerSample.ViewModels
{
    [POCOViewModel]
    public class ViewModel1
    {

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

                MyReport MyReport = new MyReport();
                MyReport.ReportDesigner.OpenDocument(report);
                MyReport.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);

            }
        }

        public void BindToData(XtraReport report)
        {
            Model1 MyContext = new Model1();

           string ConnectionString =  MyContext.Database.Connection.ConnectionString;
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(ConnectionString);

            DevExpress.DataAccess.Sql.SqlDataSource ds = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);

            CustomSqlQuery QueryMaster = new CustomSqlQuery();
            QueryMaster.Name = "Employes";
            QueryMaster.Sql = "SELECT * FROM Employes";

            CustomSqlQuery QueryChild = new CustomSqlQuery();
            QueryChild.Name = "Orders";
            QueryChild.Sql = "SELECT * FROM Orders";

            ds.Queries.AddRange(new SqlQuery[] { QueryMaster, QueryChild });
            ds.Relations.Add("QueryMaster", "QueryChild", "Id", "EmployeId");

            report.DataSource = ds;
            report.DataMember = "Employes";
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
            SetLabelProperties(labelDetail , "First Name", "FirstName");

            // Last Name
            XRLabel labelDetail2 = new XRLabel();
            SetLabelProperties(labelDetail2 , "Last Name", "LastName");

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
            label.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
            label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            label.ExpressionBindings.Add(
            new ExpressionBinding("BeforePrint", "Text", "'" + title + ": ' + [" + member + "]"));

        }

        public void CreateDetailReport(XtraReport report, string DataMember) 
        {
            DetailReportBand detailReportBand = new  DetailReportBand();
            report.Bands.Add(detailReportBand);
            detailReportBand.DataSource = report.DataSource;
            detailReportBand.DataMember = DataMember;



        }
    }


   

}