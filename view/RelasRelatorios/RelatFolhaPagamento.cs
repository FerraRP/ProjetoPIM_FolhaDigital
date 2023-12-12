using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaFigital_Projeto.view.RelasRelatorios
{
    public partial class RelatFolhaPagamento : Form
    {
        DataTable dt = new DataTable();
        public RelatFolhaPagamento(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void RelatFolhaPagamento_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DSRelatorioFolhaPag", dt));
            this.reportViewer1.RefreshReport();
        }
    }
}
