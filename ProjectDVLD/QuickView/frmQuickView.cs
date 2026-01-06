using DVLD_Buisness;
using ProjectDVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.QuickView
{
    public partial class frmQuickView: Form
    {
        public frmQuickView()
        {
            InitializeComponent();
        }

        private DataTable dtAllPeople;
        private DataTable dtAllUsers;
        private void frmQuickView_Load(object sender, EventArgs e)
        {
            dtAllPeople = clsPersonBuisnessLayer.GetAllPeople();
            labTotlePeopleNumber.Text = dtAllPeople.Rows.Count.ToString();

            dtAllUsers = clsUsersBuisnessLayer.GetInfoUsers();
            labTotleUserNumber.Text = dtAllUsers.Rows.Count.ToString();

        }
    }
}
