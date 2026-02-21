using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using ProjectDVLD;
using System.Windows.Forms;
using ProjectDVLD.Licenses.Local_Licenses;

namespace ProjectDVLD.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {

        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);
            llShowLicenseInfo.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = Global_Classes.clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = Global_Classes.clsUserInfo.CurrentUser.UserName;
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            clsDetainedLicenseBL DetainedLicense = new clsDetainedLicenseBL();

            DetainedLicense.LicenseID = _SelectedLicenseID;
            DetainedLicense.FineFees = Convert.ToDecimal( txtFineFees.Text);
            DetainedLicense.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;
            DetainedLicense.IsReleased = false;

            if (DetainedLicense.Save())
            {

                MessageBox.Show(
                    "The operation was completed successfully and the license has been detained.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                lblDetainID.Text = DetainedLicense.DetainID.ToString();
                llShowLicenseInfo.Enabled = true;

            }
                
            else
            {
                MessageBox.Show(
                    "The operation failed. The license could not be detained due to an error.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }










        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!Global_Classes.clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }
    }
}
