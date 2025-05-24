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

namespace ProjectDVLD.Applications.Local_Driving_License.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
                
        private int _LocalDrivingLicenseApplicationsID = -1;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplications;



        public void _LoadData(int LocalDrivingLicenseApplicationsID)
        {
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationsID);

            if (_LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("Error: A problem occurred while fetching the data. Please make sure all operations are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplications.ApplicationID.ToString();
            lblAppliedFor.Text = _LocalDrivingLicenseApplications.LicenseClassInfo.ClassName;
            ctrlApplicationBasicInfo1.LoadDataApp(_LocalDrivingLicenseApplications.ApplicationID);

        }


        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }




    }
}
