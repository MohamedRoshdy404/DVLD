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

namespace ProjectDVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {


        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int SelectedPersonID = -1;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicesnseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
        }


        private void _FillLicenseClassIncomboBox()
        {
            DataTable LicenseClass = clsLicenseClassBuisnessLayer.GetAllLicenseClasses();

            foreach (DataRow Class in LicenseClass.Rows)
            {
                cbLicenseClass.Items.Add(Class["ClassName"]);
            }
            cbLicenseClass.SelectedIndex = 0;

        }


        private void _LoadData()
        {

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplicationBL.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClassBuisnessLayer.FindLicenseClassesByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsUsersBuisnessLayer.Find(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

        }



        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {

            clsPersonBuisnessLayer Person = clsPersonBuisnessLayer.FindByPersonID(ctrlPersonCardWithFilter1.PersonID);

            if (Person != null)
            {
                //lblLocalDrivingLicebseApplicationID.Text = Person.PersonID.ToString();
                lblCreatedByUser.Text = clsUserInfo.UserName;
                tcApplicationInfo.SelectedTab = tpApplicationInfo;
                tcApplicationInfo.TabPages["tpApplicationInfo"].Enabled = true;
                lblApplicationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                _FillLicenseClassIncomboBox();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            // clsLicenseClassBuisnessLayer leicense = clsLicenseClassBuisnessLayer.FindLicenseClassesByClassName(cbLicenseClass.SelectedItem.ToString());

            //if (leicense == null)
            //{
            //    MessageBox.Show("Sorry, the license type was not found. Please try again correctly.", "ERROR" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            //    return;
            //}

            //lblFees.Text = leicense.ClassFees.ToString();
            lblFees.Text = clsApplicationTypeBuisnessLayer.FindApplicationType((int)clsApplicationsBuisnessLayer.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
        }
    }
}
