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
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID;


        public clsTestType.enTestType TestTypeID
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
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        break;

                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        break;


                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                        break;
                }
            }
        }

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }



        public void LoadData(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName.ToString();
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName.ToString();
            lblTrial.Text = "0";
            lblDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblFees.Text = clsTestType.Find(TestTypeID).TestTypeFees.ToString();
            lblTestID.Text = "Not Taken Yet";



        }



    }
}
