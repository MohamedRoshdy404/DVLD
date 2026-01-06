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

namespace ProjectDVLD.Tests.Test_Types
{
    public partial class frmListTestTypes: Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private static DataTable dtTestTypesList = clsTestType.GetAllInfoTestType();


        private void _RefreshApplicationTypesList()
        {
            dtTestTypesList = clsTestType.GetAllInfoTestType();
            dgvTestTypes.DataSource = dtTestTypesList;
            lblRecordsCount.Text = dgvTestTypes.RowCount.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {

            _RefreshApplicationTypesList();

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 100;
                        
            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;



        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmEditTestType = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frmEditTestType.ShowDialog();
            _RefreshApplicationTypesList(); 
        }

        private void dgvTestTypes_DoubleClick(object sender, EventArgs e)
        {
            Form frmEditTestType = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frmEditTestType.ShowDialog();
            _RefreshApplicationTypesList();
        }
    }
}
