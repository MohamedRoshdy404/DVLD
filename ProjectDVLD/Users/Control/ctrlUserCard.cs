using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace ProjectDVLD.Users.Control
{
    public partial class ctrlUserCard: UserControl
    {

        private int _PersonID;
        clsUsersBuisnessLayer _User;
        public ctrlUserCard()
        {
            InitializeComponent();


        }



        public void LoadUserInfo(int UserID)
        {
            _User = clsUsersBuisnessLayer.Find(UserID);
            if (_User == null)
            {
                //_ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive == 1)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }


        private void ctrlUserCard_Load(object sender, EventArgs e)
        {
           
        }
    }
}
