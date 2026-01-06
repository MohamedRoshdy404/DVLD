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
using DVLD_Models;

namespace ProjectDVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {

        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public frmListTestAppointments()
        {
            InitializeComponent();
        }
                
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _TestType = TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID ;
            ctrlDrivingLicenseApplicationInfo1._LoadData(LocalDrivingLicenseApplicationID);
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            Form frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID , _TestType);
            clsTestAppointmentBL appointments = clsTestAppointmentBL.GetLastTestAppointment(_LocalDrivingLicenseApplicationID,_TestType);


            if (appointments != null)
            {
                if (appointments.IsLocked = true)
                {
                    MessageBox.Show(
                            "This person has a previous appointment that has not been completed.\n" +
                            "Please complete it first before proceeding with the remaining tests.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    return;
                }
            }
            frmScheduleTest.ShowDialog();

            frmListTestAppointments_Load(null , null );




        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            _dtLicenseTestAppointments = clsTestAppointmentBL.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, (int) _TestType);
            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        }
    }
}
