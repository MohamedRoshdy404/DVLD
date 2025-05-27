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

namespace ProjectDVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        private clsTestTypeBuisnessLayer.enTestType _TestTypeID = clsTestTypeBuisnessLayer.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        //private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        private clsLocalDrivingLicenseApplicationBL _Application;
        public clsTestTypeBuisnessLayer.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }

            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestTypeBuisnessLayer.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                       pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        break;

                    case clsTestTypeBuisnessLayer.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        break;


                    case clsTestTypeBuisnessLayer.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                        break;
                }
            }
        }

        private void _FillVisionTestData()
        {
            lblLocalDrivingLicenseAppID.Text = _Application.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _Application.LicenseClassID.ToString();
            lblFullName.Text = _Application.PersonFullName.ToString();
            //lblTrial.Text = _Application.t
            clsTestTypeBuisnessLayer test = clsTestTypeBuisnessLayer.Find((int)_TestTypeID);
            lblFees.Text = test.TestTypeFees.ToString();

        }
        public void LoadData(int LocalDrivingLicenseApplicationID)
        {
             _Application = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (_Application == null)
            {
                MessageBox.Show("Failed to retrieve license data. Please try again." , "ERROR" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }

            _FillVisionTestData();




        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
