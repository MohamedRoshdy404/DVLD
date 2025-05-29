using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Buisness;
using ProjectDVLD.Tests;
using System.Windows.Forms;

namespace ProjectDVLD.Tests
{
    public partial class frmScheduleTest : Form
    {


        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypeBuisnessLayer.enTestType _TestTypeID = clsTestTypeBuisnessLayer.enTestType.VisionTest;
        private int _AppointmentID = -1;


        public frmScheduleTest()
        {
            InitializeComponent();
        }
               
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestTypeBuisnessLayer.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadData(_LocalDrivingLicenseApplicationID);
        }

        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
