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
using ProjectDVLD.Licenses.Local_Licenses;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private clsLicenseBL _License;
        private int _NewLicenseID = -1;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ValidateLocalLicenseForInternationalLicense;
            InitializeFormData();
        }

        private void ValidateLocalLicenseForInternationalLicense(int LicenseID)
        {
            _License = clsLicenseBL.Find(LicenseID);

            llShowLicenseHistory.Enabled = (LicenseID != -1);

            if (_License == null)
            {
                MessageBox.Show(
                        "An error occurred while retrieving the data.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                return;
            }

            lblLicenseFees.Text = Convert.ToInt32( clsLicenseClassBuisnessLayer.FindLicenseClassesByClassName(_License.LicenseClassIfo.ClassName).ClassFees).ToString();

            lblTotalFees.Text = ( Convert.ToInt32(lblApplicationFees.Text) + Convert.ToInt32(lblLicenseFees.Text) ).ToString();
            lblOldLicenseID.Text = _License.LicenseID.ToString();


            if (_License.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show(
                    "This license has not expired. Its expiration date is on: " + Global_Classes.clsFormat.DateToShort(_License.ExpirationDate),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }


        private void InitializeFormData()
        {
            lblApplicationDate.Text = Global_Classes.clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = Global_Classes.clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = Global_Classes.clsUserInfo.CurrentUser.UserID.ToString();
            lblApplicationFees.Text =  Convert.ToInt32( clsApplicationTypeBuisnessLayer.Find((int)clsApplicationsBuisnessLayer.enApplicationType.RenewDrivingLicense).ApplicationFees).ToString();             
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicenseBL NewLicense =
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
                Global_Classes.clsUserInfo.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            lblExpirationDate.Text = Global_Classes.clsFormat.DateToShort(NewLicense.ExpirationDate);
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }
    }
}
