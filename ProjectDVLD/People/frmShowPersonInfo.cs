using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo( int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {
            
        }
    }
}
