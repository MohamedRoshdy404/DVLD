using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using ProjectDVLD.Licenses;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            _FillDataAppInfo();

            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += getLicenseID;
        }


        private void getLicenseID(int LicenseID)
        {
            llShowLicenseHistory.Enabled = (LicenseID != -1);
            lblLocalLicenseID.Text = LicenseID.ToString();
        }

        private void _FillDataAppInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationTypeBuisnessLayer.Find(6).ApplicationFees.ToString();
            lblCreatedByUser.Text = Global_Classes.clsUserInfo.CurrentUser.UserName;
            DateTime today = DateTime.Today;
            DateTime nextYear = today.AddYears(1);
            lblExpirationDate.Text = nextYear.ToShortDateString();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
