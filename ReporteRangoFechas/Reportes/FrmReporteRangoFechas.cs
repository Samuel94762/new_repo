using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteRangoFechas.Reportes
{
    public partial class FrmReporteRangoFechas : Form
    {
        public FrmReporteRangoFechas()
        {
            InitializeComponent();
        }

        private void FrmReporteRangoFechas_Load(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
            btnAceptar.Enabled = false;

        }
        private void getSalesReport(DateTime startDate, DateTime endDate)
        {
            SalesReport reportModel = new SalesReport();
            reportModel.createSalesOrderReport(startDate, endDate);

            salesReportBindingSource.DataSource = reportModel;
            salesListingBindingSource.DataSource = reportModel.salesListing;
            netSalesByPeriodBindingSource.DataSource= reportModel.netSalesByPeriod;

            this.reportViewer1.RefreshReport();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            btnAceptar.Enabled = true;
            dtpFromDate.Enabled = true;
            dtpToDate.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            string fromDate = "03/11/2019 00:00:00";
            string toDate = "03/11/2019 13:31:00";
            getSalesReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
        }

        private DateTime FakeToday = Convert.ToDateTime("03/11/2019 13:31:00");

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            var fromDate = FakeToday.AddDays(-7);
            var toDate = FakeToday;
            getSalesReport(fromDate, toDate);

        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            var fromDate = new DateTime(FakeToday.Year, FakeToday.Month, 1);
            var toDate = FakeToday;
            getSalesReport(fromDate, toDate);

        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            var fromDate = FakeToday.AddDays(-30);
            var toDate = FakeToday;
            getSalesReport(fromDate, toDate);
        }

        private void btnThisYear_Click(object sender, EventArgs e)
        {
            var fromDate = new DateTime(FakeToday.Year, 1, 1);
            var toDate = FakeToday;
            getSalesReport(fromDate, toDate);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var fromDate = dtpFromDate.Value;
            var toDate = dtpToDate.Value;
            getSalesReport(fromDate, new DateTime(toDate.Year, toDate.Month, toDate.Day, 23,59,59));
        }
    }
}
