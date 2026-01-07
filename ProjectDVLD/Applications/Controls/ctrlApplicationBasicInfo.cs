using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDVLD.Global_Classes;
using ProjectDVLD.People;
using DVLD_Buisness;
using System.Windows.Forms;

namespace ProjectDVLD.Applications.Application_Types.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        private int _ApplicationID;
        private clsApplicationsBuisnessLayer Application;

        public void ResetApplicationInfo()
        {
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";

        }

        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = Application.ApplicationID.ToString();
            lblStatus.Text = Application.ApplicationStatus.ToString();
            lblFees.Text = Application.PaidFees.ToString();
            lblType.Text = Application.ApplicationTypeInfo.ApplicationTypeTitle.ToString();
            lblApplicant.Text = Application.ApplicantFullName;
            lblDate.Text = clsFormat.DateToShort(Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort( Application.LastStatusDate);
            lblCreatedByUser.Text = Application.CreatedByUserInfo.UserName;
        }
        public void LoadApplicationInfo(int AppID)
        {
            _ApplicationID = AppID;
            Application = clsApplicationsBuisnessLayer.FindBaseApplication(_ApplicationID);
            if (Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("Error: A problem occurred while fetching the data. Please make sure all operations are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();


        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form ShowPersonInfo = new frmShowPersonInfo(Application.ApplicantPersonID);
            ShowPersonInfo.ShowDialog();
            LoadApplicationInfo(_ApplicationID);

        }
    }
}
