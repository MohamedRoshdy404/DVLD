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
using ProjectDVLD.Licenses.International_Licenses; 
using System.Windows.Forms;

namespace ProjectDVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {

        private clsLicenseBL _License;
        private clsInternationalLicenseBL _InternationalLicense;
        private int _InternationalLicenseID;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            _FillDataAppInfo();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ValidateLocalLicenseForInternationalLicense;
        }


        private void ValidateLocalLicenseForInternationalLicense(int LicenseID)
        {
            btnIssueLicense.Enabled = true;
            llShowLicenseHistory.Enabled = (LicenseID != -1);
            lblLocalLicenseID.Text = LicenseID.ToString();

            string error = "";
            if (!clsInternationalLicenseBL.CanIssueInternationalLicense(LicenseID, ref error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueLicense.Enabled = false;
                return;
            }

        }

        private void _FillDataAppInfo()
        {
            lblApplicationDate.Text = Global_Classes.clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = Global_Classes.clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationTypeBuisnessLayer.Find(6).ApplicationFees.ToString();
            lblCreatedByUser.Text = Global_Classes.clsUserInfo.CurrentUser.UserName;
            DateTime today = DateTime.Today;
            DateTime nextYear = today.AddYears(1);
            lblExpirationDate.Text = Global_Classes.clsFormat.DateToShort(nextYear);
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



            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            _InternationalLicense = new clsInternationalLicenseBL();
            _InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            _InternationalLicense.ApplicationDate = DateTime.Now;
            _InternationalLicense.ApplicationStatus = clsApplicationsBuisnessLayer.enApplicationStatus.Completed;
            _InternationalLicense.LastStatusDate = DateTime.Now;
            _InternationalLicense.PaidFees = clsApplicationTypeBuisnessLayer.Find((int)clsApplicationsBuisnessLayer.enApplicationType.NewInternationalLicense).ApplicationFees;
            //InternationalLicense.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;


            _InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicense.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;

            if (!_InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = _InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + _InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicense.DriverID);
            frm.ShowDialog();
        }
    }
}
