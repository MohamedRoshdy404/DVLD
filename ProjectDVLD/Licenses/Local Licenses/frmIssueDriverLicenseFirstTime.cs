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


            int LicenseID = _LocalDrivingLicenseApplications.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),Global_Classes.clsUserInfo.CurrentUser.UserID);


            if (LicenseID != 0)
            {
                if (LicenseID != -1)
                {
                    MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                        "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("License Was not Issued ! ",
                     "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            btnIssueLicense.Enabled = false;



            ;
        }
    }
}
