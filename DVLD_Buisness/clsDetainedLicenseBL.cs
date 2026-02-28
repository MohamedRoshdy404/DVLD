using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DVLD_DataAccess;
using System.Data;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDetainedLicenseBL
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public decimal FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUsersBuisnessLayer CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUsersBuisnessLayer ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicenseBL()

        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;

        }

        public clsDetainedLicenseBL(int DetainID,
            int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)

        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsersBuisnessLayer.Find(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUsersBuisnessLayer.FindByPersonID(this.ReleasedByUserID);
            Mode = enMode.Update;
        }



        public static clsDetainedLicenseBL FindDetainedLicenseByLicenseID(int LicenseID)
        {

            int DetainID = 0;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;


            if (clsDetainedLicenseDA.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicenseBL(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);

            else
                return null;


        }


        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseDA.GetAllDetainedLicenses();
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseDA.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseDA.UpdateDetainedLicense(
                this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseDA.IsLicenseDetained(LicenseID);
        }


        public bool ReleaseDetainedLicense(int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
           int ApplicationStatus, DateTime LastStatusDate,
             decimal PaidFees, int CreatedByUserID, int ReleasedByUserID , ref int ApplicationID)
        {
            return clsDetainedLicenseDA.ReleaseDetainedLicense(ApplicantPersonID , ApplicationDate , ApplicationTypeID , ApplicationStatus , LastStatusDate , PaidFees , CreatedByUserID ,  this.DetainID,
                   ReleasedByUserID , ref ApplicationID);
        }
        

        //public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        //{
        //    return clsDetainedLicenseDA.ReleaseDetainedLicense(this.DetainID,
        //           ReleasedByUserID, ReleaseApplicationID);
        //}

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }



    }
}
