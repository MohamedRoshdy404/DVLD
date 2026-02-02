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

        private clsLicenseBL _License;
        private int _InternationalLicenseID;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            _FillDataAppInfo();

            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ValidateLocalLicenseForInternationalLicense;
        }


        private void ValidateLocalLicenseForInternationalLicense(int LicenseID)
        {
            string error = "";
            if (!clsInternationalLicenseBL.CanIssueInternationalLicense(LicenseID, out error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueLicense.Enabled = false;
                return;
            }


            btnIssueLicense.Enabled = true;
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

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicenseBL InternationalLicense = new clsInternationalLicenseBL();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplicationsBuisnessLayer.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypeBuisnessLayer.Find((int)clsApplicationsBuisnessLayer.enApplicationType.NewInternationalLicense).ApplicationFees;
            //InternationalLicense.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }
    }
}
