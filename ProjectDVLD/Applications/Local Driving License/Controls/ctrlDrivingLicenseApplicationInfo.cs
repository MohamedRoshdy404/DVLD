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
                
        //public ctrlDrivingLicenseApplicationInfo()
        //{
        //    InitializeComponent();
        //    //_LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
        //}

        private int _LocalDrivingLicenseApplicationsID = -1;



        public void _LoadData(int LocalDrivingLicenseApplicationsID)
        {
            clsLocalDrivingLicenseApplicationBL Applications = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationsID);

            if (Applications == null)
            {
                MessageBox.Show("Error: A problem occurred while fetching the data. Please make sure all operations are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            ctrlApplicationBasicInfo1.LoadDataApp(Applications.ApplicationID);
            lblLocalDrivingLicenseApplicationID.Text = Applications.ApplicationID.ToString();
            lblAppliedFor.Text = Applications.LicenseClassInfo.ClassName;


        }


        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }




    }
}
