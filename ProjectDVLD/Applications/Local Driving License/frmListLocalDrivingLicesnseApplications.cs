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
using ProjectDVLD.Licenses.Local_Licenses;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frmAddUpdateLocalDrivingLicesnseApplication = new frmAddUpdateLocalDrivingLicesnseApplication();
            frmAddUpdateLocalDrivingLicesnseApplication.ShowDialog();
            GetLocalDrivingLicesnseApplications();
        }

        private void GetLocalDrivingLicesnseApplications()
        {
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplicationBL.GetAllLocalDrivingLicenseApplications();
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private static DataTable _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplicationBL.GetAllLocalDrivingLicenseApplications();

        private DataTable _dtLocalDrivingLicenseApplications = _dtAllLocalDrivingLicenseApplications.DefaultView.ToTable(false, "LocalDrivingLicenseApplicationID" , "NationalNo" , "FullName" , "Status");
        
        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicenseApplicationBL.GetAllLocalDrivingLicenseApplications();
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 280;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 120;
            }

            cbFilterBy.SelectedIndex = 0;
            //_TestTakingLogic();
        }


        private void _TestTakingLogic()
        {

            if (dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value.ToString() == "New")
            {
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }
            else
            {
                MessageBox.Show("ERROR");
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";


            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }


            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")

                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text);



            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmLocalDrivingLicenseApplicationInfo = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmLocalDrivingLicenseApplicationInfo.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddUpdateLocalDrivingLicesnseApplication = new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmAddUpdateLocalDrivingLicesnseApplication.ShowDialog();
            GetLocalDrivingLicesnseApplications();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplicationBL LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    GetLocalDrivingLicesnseApplications();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplicationBL LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    GetLocalDrivingLicesnseApplications();
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            GetLocalDrivingLicesnseApplications();

        }


        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void dgvLocalDrivingLicenseApplications_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplicationBL LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;
            showLicenseToolStripMenuItem.Enabled = LicenseExists;

            editToolStripMenuItem.Enabled  = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplicationsBuisnessLayer.enApplicationStatus.New);
            DeleteApplicationToolStripMenuItem.Enabled = TotalPassedTests == 0;
            CancelApplicaitonToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplicationsBuisnessLayer.enApplicationStatus.New);


            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && LocalDrivingLicenseApplication
                .ApplicationStatus == clsApplicationsBuisnessLayer.enApplicationStatus.New;


            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && (!PassedWrittenTest);
                scheduleStreetTestToolStripMenuItem.Enabled = PassedWrittenTest && (!PassedStreetTest);
            }


        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            GetLocalDrivingLicesnseApplications();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {



            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            //int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            //frmShowLicenseInfo frm = new frmShowLicenseInfo(LocalDrivingLicenseApplicationID);
            //frm.ShowDialog();
            GetLocalDrivingLicesnseApplications();
        }
    }
}
