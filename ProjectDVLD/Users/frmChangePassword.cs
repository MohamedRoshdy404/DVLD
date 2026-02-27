using DVLD_Buisness;
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
    public partial class frmChangePassword : Form
    {

        private int _UserID;
        private clsUsersBuisnessLayer _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field must not be empty.");
            }
            else
            {

                if (txtCurrentPassword.Text != _User.Password)
                {
                    e.Cancel = true;
                    Temp.Focus();
                    errorProvider1.SetError(Temp, "Password confirmation does not match password.");
                }


                if (txtConfirmPassword.Text != txtPassword.Text)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RestTextBoxValue()
        {
            txtCurrentPassword.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }



        private void frmChangePassword_Load(object sender, EventArgs e)
        {

            _User = clsUsersBuisnessLayer.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnSave.Focus();
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field must not be empty.");
            }
            else
            {

                if (Global_Classes.clsUtil.ComPuteHash(txtCurrentPassword.Text) != _User.Password)
                {
                    e.Cancel = true;
                    Temp.Focus();
                    errorProvider1.SetError(Temp, "Password confirmation does not match password.");
                }

            }




        }




        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (txtConfirmPassword.Text != txtPassword.Text || Global_Classes.clsUtil.ComPuteHash(txtCurrentPassword.Text) != _User.Password)
            {
                MessageBox.Show(" The password field does not match the Password Configuration field.", "Data NOT Saved.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            _User.Password = Global_Classes.clsUtil.ComPuteHash(txtPassword.Text);
            //_User.Password = txtPassword.Text;

            if (_User.ChangePassword())
            {
                MessageBox.Show($"Password Chenged Successfully.", "Done Seved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RestTextBoxValue();

            }
            else
            {
                MessageBox.Show("An error occurred while changing the password.", "Error in the password change process.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _RestTextBoxValue();
            }


        }
    }
}
