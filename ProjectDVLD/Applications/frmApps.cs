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

namespace ProjectDVLD.Applications
{
    public partial class frmApps : Form
    {
        public frmApps()
        {
            InitializeComponent();
        }

        private void frmApps_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsApplicationsBuisnessLayer.GetAllApplications();
            
        }
    }
}
