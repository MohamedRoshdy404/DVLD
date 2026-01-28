using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsDriverBL
    {

        private int _LocalDrivingLicenseApplicationsID = -1;
        private clsLocalDrivingLicenseApplicationBL _LocalDrivingLicenseApplications;
        private clsPersonBuisnessLayer _Person;
        private clsLicenseBL _License;

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPersonBuisnessLayer PersonInfo;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }

        public clsDriverBL()

        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }

        public clsDriverBL(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPersonBuisnessLayer.FindByPersonID(PersonID);

            Mode = enMode.Update;
        }



        public static DataTable GetLicenses(int DriverID)
        {
            return clsDriverDA.GetDriverLicenses(DriverID);
        }


        public static clsDriverBL FindByPersonID(int PersonID)
        {
            int driverID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            if (clsDriverDA.GetDriverInfoByPersonID(
                PersonID,
                ref driverID,
                ref createdByUserID,
                ref createdDate))
            {
                return new clsDriverBL(driverID, PersonID, createdByUserID, createdDate);
            }

            return null;
        }


        public static clsDriverBL FindByDriverID(int DriverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.MinValue;

            if (clsDriverDA.GetDriverInfoByDriverID(DriverID, ref personID,ref createdByUserID,ref createdDate))
            {
                return new clsDriverBL(DriverID,personID,createdByUserID,createdDate);
            }

            return null;
        }


        //public bool SavePersonAndLicenseWithChecks()
        //{

        //}




        public int GetDriverIDByPersonID(int PersonID)
        {
            return clsDriverDA.GetDriverIDByPersonID(PersonID);
        }

        public bool IsDriverExistByPersonID(int PersonID)
        {
            return clsDriverDA.IsDriverExistByPersonID(PersonID);
        }


        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverDA.AddNewDriver(PersonID, CreatedByUserID);
            return (this.DriverID != -1);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case enMode.Update:

                //    return _UpdateDriver();

            }

            return false;
        }



    }
}
