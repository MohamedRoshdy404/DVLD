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
                
        public ctrlDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationsID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
        }

        private int _LocalDrivingLicenseApplicationsID = -1;

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            clsApplicationsBuisnessLayer Applications = clsApplicationsBuisnessLayer.FindBaseApplication(_LocalDrivingLicenseApplicationsID);

            if (Applications != null)
            {
                lblLocalDrivingLicenseApplicationID.Text = Applications.ApplicationID.ToString();
            }
        }
    }
}
