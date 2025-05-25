using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        public frmListTestAppointments()
        {
            InitializeComponent();
        }
                
        public frmListTestAppointments(int LocalID)
        {
            InitializeComponent();
            ctrlDrivingLicenseApplicationInfo1._LoadData(LocalID);
        }







    }
}
