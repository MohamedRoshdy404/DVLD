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
                       // pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;

                    case clsTestTypeBuisnessLayer.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        //pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;


                    case clsTestTypeBuisnessLayer.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        //pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                }
            }
        }


        public ctrlScheduleTest()
        {
            InitializeComponent();
        }





    }
}
