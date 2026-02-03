using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDVLD.Licenses;
using ProjectDVLD.Licenses.International_Licenses ;
using ProjectDVLD.People;
using DVLD_Buisness;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private clsDriverBL _Driver;
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

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int) dgvInternationalLicenses.CurrentRow.Cells[2].Value;

            _Driver = clsDriverBL.FindByDriverID(DriverID);

            if (_Driver != null)
            {
                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_Driver.PersonID);
                frm.ShowDialog();
            }


        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;

            _Driver = clsDriverBL.FindByDriverID(DriverID);

            if (_Driver != null)
            {
                frmShowPersonInfo frm = new frmShowPersonInfo(_Driver.PersonID);
                frm.ShowDialog();
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo (DriverID);
            frm.ShowDialog();
        }
    }
}
