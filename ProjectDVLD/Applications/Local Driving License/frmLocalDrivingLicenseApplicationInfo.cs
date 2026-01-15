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

namespace ProjectDVLD.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        public frmLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationsID)
        {
            InitializeComponent();
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LocalDrivingLicenseApplicationsID);
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
