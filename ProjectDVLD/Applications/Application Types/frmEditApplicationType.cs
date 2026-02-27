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
    public partial class frmEditApplicationType: Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypeBuisnessLayer _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {

            _ApplicationType = clsApplicationTypeBuisnessLayer.Find(_ApplicationTypeID);

            if (_ApplicationType == null)
            {

                MessageBox.Show(" An error occurred while searching for the application ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToDecimal(txtFees.Text);

            if (_ApplicationType.UpdateApplicationType())
                MessageBox.Show("The update was completed successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
                MessageBox.Show("The update process failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
