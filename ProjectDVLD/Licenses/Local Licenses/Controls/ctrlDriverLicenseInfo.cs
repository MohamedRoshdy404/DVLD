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

namespace ProjectDVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {

        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplication;
        private clsPersonBuisnessLayer _Person;
        private clsLicenseBL _LicenseClass;
        private int _LocalDrivingLicenseApplicationID;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }








        public void LoadDataPersonWithLicense(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show(
                    "An error occurred while retrieving the application data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _Person = clsPersonBuisnessLayer.FindByPersonID(_LocalDrivingLicenseApplication.ApplicantPersonID);

            if (_Person == null)
            {
                                MessageBox.Show(
                    "An error occurred while retrieving the person's data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }


            _LicenseClass = clsLicenseBL.FindLicenseInfoByLicenseID(_LocalDrivingLicenseApplication.ApplicationID, _LocalDrivingLicenseApplication.LicenseClassID);

            if (_LicenseClass == null)
            {
                                MessageBox.Show(
                    "An error occurred while retrieving the license data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return ;
            }


            lblClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblLicenseID.Text = _LicenseClass.LicenseID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            if (_Person.Gender == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            lblIssueDate.Text =  _LicenseClass.IssueDate.ToShortDateString();
            lblIssueReason.Text = _LicenseClass.IssueReason.ToString();

            if (_LicenseClass.Notes == "")
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = _LicenseClass.Notes;

            if (_LicenseClass.IsActive == true)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _LicenseClass.DriverID.ToString();
            lblExpirationDate.Text = _LicenseClass.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = "No";





            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }











        }




    }
}
