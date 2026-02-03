using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            dgvInternationalLicenses.DataSource = clsInternationalLicenseBL.GetAllInternationalLicenses();
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.RowCount.ToString();
        }
    }
}
