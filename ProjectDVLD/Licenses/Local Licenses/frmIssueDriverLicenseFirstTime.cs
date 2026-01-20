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

namespace ProjectDVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationsID = -1;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplications;
        private clsPersonBuisnessLayer _Person;
        private clsLicenseBL _License;
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationsID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationsID);
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationsID);
            if (_LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("This application does not exist on the system.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (clsLicenseBL.GetActiveLicenseIDByPersonID(_LocalDrivingLicenseApplications.ApplicantPersonID, _LocalDrivingLicenseApplications.LicenseClassID) > 1)
            {
                MessageBox.Show("The Person Is Active Liiii");
            }

            clsDriverBL Driver = new clsDriverBL();

            Driver.PersonID = _LocalDrivingLicenseApplications.ApplicantPersonID;
            Driver.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;


            _Person = clsPersonBuisnessLayer.FindByPersonID(_LocalDrivingLicenseApplications.ApplicantPersonID);

            DateTime datePerson = _Person.DateOfBirth;
            DateTime dtNow = DateTime.Now;
            int Years = (datePerson.Year - dtNow.Year) * -1 ;

            
            if (Years >= _LocalDrivingLicenseApplications.LicenseClassInfo.MinimumAllowedAge)
            {
                if (Driver.Save())
                {
                    MessageBox.Show(
                        "New driver added successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    _License = new clsLicenseBL();
                    _License.ApplicationID = _LocalDrivingLicenseApplications.ApplicationID;
                    _License.DriverID = Driver.DriverID;
                    _License.LicenseClass = _LocalDrivingLicenseApplications.LicenseClassID;
                    _License.IssueDate = DateTime.Now;
                    DateTime now = DateTime.Now; // تاريخ ووقت الحالي
                    _License.ExpirationDate = now.AddYears(_LocalDrivingLicenseApplications.LicenseClassInfo.DefaultValidityLength);
                    _License.Notes = txtNotes.Text;
                    _License.PaidFees = _LocalDrivingLicenseApplications.LicenseClassInfo.ClassFees;
                    _License.IsActive = true;
                    _License.IssueReason = clsLicenseBL.enIssueReason.FirstTime;
                    _License.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;

                    if (_License.Save())
                        MessageBox.Show(
                        "New license added successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );


                    else
                        MessageBox.Show(
                                "License issuance failed. Please follow the steps correctly.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );


                }
                else
                {
                    MessageBox.Show(
                        "Failed to issue a new driver.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );


                }
            }
            else
            {
                MessageBox.Show(
                    "The applicant is under the required age and is not eligible for this license.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

            }







            btnIssueLicense.Enabled = false;



            ;
        }
    }
}
