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

namespace ProjectDVLD.Tests
{
    public partial class frmTakeTest : Form
    {

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID;
        private int _TestAppointmentID;
        private clsTestAppointmentBL _TestAppointment;
        private clsTestBL _Test;
        public frmTakeTest(int LocalDrivingLicenseApplicationID , clsTestType.enTestType TestType , int AppointmentID)
        {
            InitializeComponent();
            _TestType = TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _TestType;
            ctrlSecheduledTest1.LoadData(_LocalDrivingLicenseApplicationID);
            _TestAppointment = clsTestAppointmentBL.Find(_TestAppointmentID);

            if (_TestAppointment.IsLocked == true)
            {
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                btnSave.Enabled = false;
                txtNotes.Enabled = false;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
               )
            {
                return;
            }

            _Test = new clsTestBL();

            _TestAppointment.IsLocked = true;
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = Global_Classes.clsUserInfo.CurrentUser.UserID;

            if (_Test.Save() && _TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
