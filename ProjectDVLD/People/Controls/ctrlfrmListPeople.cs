using DVLD_Buisness;
using ProjectDVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.UserControls
{
    public partial class ctrlfrmListPeople : UserControl
    {


        private static DataTable _dtAllPeople = clsPersonBuisnessLayer.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false , "PersonID", "NationalNo" , "FirstName" , "SecondName" , "ThirdName" , "LastName" , "Gender" , "DateOfBirth" , "CountryName" , "Phone" , "Email");



        public ctrlfrmListPeople()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
            _RefreshPeoplList();
        }

        private void _RefreshPeoplList()
        {
            txtFilter.Visible = false;
            cbFilterBy.SelectedIndex = 0;
            _dtAllPeople = clsPersonBuisnessLayer.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");
            DGVGetAllPeople.DataSource = _dtPeople;
            labRecordsCount.Text = DGVGetAllPeople.Rows.Count.ToString();

        }
        private void picBoxAddPerson_Click(object sender, EventArgs e)
        {
            Form frmAddUpdatePerson = new frmAddUpdatePerson();
            frmAddUpdatePerson.ShowDialog();
            _RefreshPeoplList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmShowPersonInfo = new frmShowPersonInfo( (int)DGVGetAllPeople.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();
            _RefreshPeoplList();
        }

        private void ctrlfrmListPeople_Load(object sender, EventArgs e)
        {



            DGVGetAllPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            labRecordsCount.Text = _dtPeople.Rows.Count.ToString();

            if (DGVGetAllPeople.Rows.Count > 0)
            {

                DGVGetAllPeople.Columns[0].HeaderText = "Person ID";
                DGVGetAllPeople.Columns[0].Width = 110;

                DGVGetAllPeople.Columns[1].HeaderText = "National No";
                DGVGetAllPeople.Columns[1].Width = 150;

                DGVGetAllPeople.Columns[2].HeaderText = "First Name";
                DGVGetAllPeople.Columns[2].Width = 120;

                DGVGetAllPeople.Columns[3].HeaderText = "Second Name";
                DGVGetAllPeople.Columns[3].Width = 140;

                DGVGetAllPeople.Columns[4].HeaderText = "Third Name";
                DGVGetAllPeople.Columns[4].Width = 150;

                DGVGetAllPeople.Columns[5].HeaderText = "Last Name";
                DGVGetAllPeople.Columns[5].Width = 120;

                DGVGetAllPeople.Columns[6].HeaderText = "Gender";
                DGVGetAllPeople.Columns[6].Width = 120;

                DGVGetAllPeople.Columns[7].HeaderText = "Date Of Birth";
                DGVGetAllPeople.Columns[7].Width = 140;

                DGVGetAllPeople.Columns[8].HeaderText = "Nationality";
                DGVGetAllPeople.Columns[8].Width = 120;

                DGVGetAllPeople.Columns[9].HeaderText = "Phone";
                DGVGetAllPeople.Columns[9].Width = 120;

                DGVGetAllPeople.Columns[10].HeaderText = "Email";
                DGVGetAllPeople.Columns[10].Width = 170;

            }


         
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmUpdatePerson = new frmAddUpdatePerson((int)DGVGetAllPeople.CurrentRow.Cells[0].Value);
            frmUpdatePerson.ShowDialog();
            _RefreshPeoplList();
        }

        private void AddNewPersontoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddPerson = new frmAddUpdatePerson();
            frmAddPerson.ShowDialog();
            _RefreshPeoplList();
        }

        private void DGVGetAllPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frmShowPersonInfo = new frmShowPersonInfo((int)DGVGetAllPeople.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();
            _RefreshPeoplList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Person [" + DGVGetAllPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPersonBuisnessLayer.DeletePerson((int)DGVGetAllPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeoplList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available now. It will be available later" , "Note " , MessageBoxButtons.OK , MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not available now. It will be available later", "Note ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "Person ID":

                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "Gender";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }


            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                labRecordsCount.Text = _dtPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
            
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}" , FilterColumn , txtFilter.Text);
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn , txtFilter.Text);

            labRecordsCount.Text = DGVGetAllPeople.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterBy.Text != "None");

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


    }
}
