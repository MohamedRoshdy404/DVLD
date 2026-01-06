using DVLD_Buisness;
using ProjectDVLD.People;
using ProjectDVLD.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode;
        private int _UserID = -1;
        clsUsersBuisnessLayer _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
                
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _UserID = UserID;

        }

        public int PersonID
        {
            get { return ctrlPersonCardWithFilter1.PersonID; }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {


            if (Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tabPage2;
                btnSave.Enabled = true;
                tabControl1.TabPages["tabPage2"].Enabled = true;
                return;
            }


            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {


                if (clsUsersBuisnessLayer.isExist(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    tabControl1.SelectedTab = tabPage2;
                    btnSave.Enabled = true;
                    tabControl1.TabPages["tabPage2"].Enabled = true;
                }

            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }


        }

        
        
        private void _ResetDefualtValues()
        {
            btnSave.Enabled = false;
            tabControl1.TabPages["tabPage2"].Enabled = false;

            if (Mode == enMode.AddNew)
            {
                _User = new clsUsersBuisnessLayer();
                lblTitle.Text = "Add New User";

            }
            else
            {
                lblTitle.Text = "Update User";
            }
        }
        private void _LoadData()
        {

            _User = clsUsersBuisnessLayer.Find(_UserID);
            //tabControl1.TabPages["tabPage2"].Enabled = true;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = Convert.ToBoolean( _User.IsActive);
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;


        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show(" The password field does not match the Password Configuration field.", "Data NOT Saved.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.PersonID = PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = Global_Classes.clsUtil.ComPuteHash(txtPassword.Text); ;

            if (chkIsActive.Checked)
                _User.IsActive = 1;
            else
                _User.IsActive = 0;

            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUserID.Text = _User.UserID.ToString();
            }
            else
            {
                MessageBox.Show("Data NOT Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            _ResetDefualtValues();
            if (Mode == enMode.Update)
                _LoadData();

        }


        private void txtPassword_Validating(object sender,  CancelEventArgs e)
        {
            TextBox Temp  = ((TextBox)sender);
            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field must not be empty.");
            }
            else
            {
                if(txtConfirmPassword.Text != txtPassword.Text )         
                {
                    //e.Cancel = true;
                    //Temp.Focus();
                    errorProvider1.SetError(Temp, "Password confirmation does not match password.");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtConfirmPassword, "");
                }

            }

        }


    }
}
