using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.Licenses.Local_Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        public frmShowLicenseInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadDataPersonWithLicense(_LocalDrivingLicenseApplicationID);
        }
    }
}
