using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DVLD_DataAccess;
using System.Threading.Tasks;

namespace DVLD_Buisness
{

    public class clsInternationalLicenseBL : clsApplicationsBuisnessLayer
    {
        private clsLicenseBL _License;
        private int _InternationalLicenseID;


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDriverBL DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }


        public clsInternationalLicenseBL()
        {
            this.ApplicationTypeID = (int)clsApplicationsBuisnessLayer.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        public clsInternationalLicenseBL(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             decimal PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
            //this is for the base clase
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplicationsBuisnessLayer.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriverBL.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseDA.AddInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID , this.IssueDate , this.ExpirationDate , this.IsActive , this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }









        public static bool CanIssueInternationalLicense(int licenseID, out string errorMessage)
        {
            clsLicenseBL license = clsLicenseBL.Find(licenseID);

            if (license == null)
            {
                errorMessage = "This person does not hold a local license.";
                return false;
            }

            if (!license.IsActive)
            {
                errorMessage = "Local license is not active.";
                return false;
            }

            if (license.ExpirationDate < DateTime.Now)
            {
                errorMessage = "Local license has expired.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }












        public bool Save()
        {

            base.Mode = (clsApplicationsBuisnessLayer.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }






            return false;

        }



    }
}
