﻿using System;
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
    public partial class frmListTestAppointments : Form
    {

        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestTypeBuisnessLayer.enTestType _TestType = clsTestTypeBuisnessLayer.enTestType.VisionTest;

        public frmListTestAppointments()
        {
            InitializeComponent();
        }
                
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypeBuisnessLayer.enTestType TestType)
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

                case clsTestTypeBuisnessLayer.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Properties.Resources.Vision_512;
                        break;
                    }

                case clsTestTypeBuisnessLayer.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypeBuisnessLayer.enTestType.StreetTest:
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
            frmScheduleTest.ShowDialog();
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            dgvLicenseTestAppointments.DataSource = clsTestAppointmentBL.GetTestAppointmentInfoByID(_LocalDrivingLicenseApplicationID ,(int) _TestType);
        }
    }
}
