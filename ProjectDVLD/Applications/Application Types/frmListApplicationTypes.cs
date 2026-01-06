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

namespace ProjectDVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes: Form
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private static DataTable dtApplicationTypesList = clsApplicationTypeBuisnessLayer.GetAllApplicationType();
        private void _RefreshApplicationTypesList()
        {
            dtApplicationTypesList = clsApplicationTypeBuisnessLayer.GetAllApplicationType();
            dgvApplicationTypes.DataSource = dtApplicationTypesList;
            lblRecordsCount.Text = dtApplicationTypesList.Rows.Count.ToString();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 100;          
            
            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmEditApplicationType = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value );
            frmEditApplicationType.ShowDialog();
            _RefreshApplicationTypesList();
        }

        private void dgvApplicationTypes_DoubleClick(object sender, EventArgs e)
        {
            Form frmEditApplicationType = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frmEditApplicationType.ShowDialog();
            _RefreshApplicationTypesList();
        }
    }
}
