using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using ProjectDVLD.Licenses.Local_Licenses;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.Local_Driving_License.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {

        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplication;
        private clsLicenseBL _License;

        private int _LocalDrivingLicenseApplicationID = -1;

        private int _LicenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
                
        private int _LocalDrivingLicenseApplicationsID = -1;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplications;

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }


        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            //incase there is license enable the show link.
            llShowLicenceInfo.Enabled = (_LicenseID != -1);


            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClassBuisnessLayer.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
           lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";


        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }
    }
}
