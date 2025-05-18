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
using ProjectDVLD.Users;
using ProjectDVLD.Global_Classes;
using ProjectDVLD.Login;
using ProjectDVLD.Applications.Application_Types;
using ProjectDVLD.Tests.Test_Types;
using ProjectDVLD.QuickView;
using ProjectDVLD.Applications.Local_Driving_License;

namespace ProjectDVLD
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lbtxtUserName.Text =  clsUserInfo.UserName.ToUpper();
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
            Form frmUserInfo = new frmUserInfo(clsUserInfo.UserID);
            frmUserInfo.ShowDialog();    
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmChangePassword = new frmChangePassword(clsUserInfo.UserID);
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
    }
}
