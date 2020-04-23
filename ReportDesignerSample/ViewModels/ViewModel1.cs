using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using ReportDesignerSample.Views;

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

             

                MyReport MyReport1 = new MyReport();

                MyReport1.ReportDesigner.OpenDocument(report);

                MyReport1.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);

            }
        }

        public void CreateDetail(XtraReport report)
        {

            // AppNum
            XRLabel labelDetail = new XRLabel();
            SetLabelProperties(labelDetail, "AppNum");

            // Note
            XRLabel labelDetail2 = new XRLabel();
            SetLabelProperties(labelDetail2, "Note");

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

        public void SetLabelProperties(XRLabel label, String member)
        {

            label.Font = new Font("Tahoma", 10, System.Drawing.FontStyle.Regular);
            label.WidthF = 200F;
            label.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
            label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            label.ExpressionBindings.Add(
            new ExpressionBinding("BeforePrint", "Text", "'" + member.ToString() + ": ' + [" + member + "]"));

        }

    }


   

}