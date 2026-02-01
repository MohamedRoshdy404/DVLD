using ProjectDVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DVLD_Buisness ;
using ProjectDVLD.Users;
using ProjectDVLD.Global_Classes;
using ProjectDVLD.Login;
using ProjectDVLD.Applications.Application_Types;
using ProjectDVLD.Tests.Test_Types;
using ProjectDVLD.QuickView;
using ProjectDVLD.Drivers;
using ProjectDVLD.Applications.International_License;
using ProjectDVLD.Applications.Local_Driving_License;

namespace ProjectDVLD
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            lebFillUserID.Text = clsUserInfo.CurrentUser.UserID.ToString();
            lebFillUsername.Text = clsUserInfo.CurrentUser.UserName.ToUpper();
            PictureBoxImgUser.ImageLocation = clsPersonBuisnessLayer.FindByPersonID(clsUserInfo.CurrentUser.PersonID).ImagePath;
        }

        private void peopalToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            Form frmListPeople = new frmListPeople();
            frmListPeople.ShowDialog();
        }

        private void ssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form frmShowPersonInfo = new frmFindPerson();
            //frmShowPersonInfo.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmGetAllUsers = new frmGetAllUsers();
            frmGetAllUsers.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            clsUserInfo.CurrentUser = null;
            Form frmLogin = new frmLogin();
            frmLogin.ShowDialog();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmUserInfo = new frmUserInfo(clsUserInfo.CurrentUser.UserID);
            frmUserInfo.ShowDialog();    
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmChangePassword = new frmChangePassword(clsUserInfo.CurrentUser.UserID);
            frmChangePassword.ShowDialog();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmListApplicationTypes = new frmListApplicationTypes();
            frmListApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmListTestTypes = new frmListTestTypes();
            frmListTestTypes.ShowDialog();
        }

        private void QoickViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmQuickView = new frmQuickView();
            frmQuickView.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form NewLocalDrivingLicesnseApplication = new frmAddUpdateLocalDrivingLicesnseApplication();
            NewLocalDrivingLicesnseApplication.ShowDialog();

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmListLocalDrivingLicesnseApplications = new frmListLocalDrivingLicesnseApplications();
            frmListLocalDrivingLicesnseApplications.ShowDialog();
        }

        private void ChkBoxShowInfoUser_CheckedChanged(object sender, EventArgs e)
        {
            PanelShowInfoUser.Visible = ChkBoxShowInfoUser.Checked;
        }

        private void PictureBoxImgUser_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo(clsUserInfo.CurrentUser.UserID);
            frmUserInfo.ShowDialog();

            lebFillUserID.Text = clsUserInfo.CurrentUser.UserID.ToString();
            lebFillUsername.Text = clsUserInfo.CurrentUser.UserName.ToUpper();
            PictureBoxImgUser.ImageLocation = clsPersonBuisnessLayer.FindByPersonID(clsUserInfo.CurrentUser.PersonID).ImagePath;
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }
    }
}
