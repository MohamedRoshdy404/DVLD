using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Models;
namespace DVLD_Buisness
{
    public class clsTestAppointmentBL
    {

        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplicationsBuisnessLayer RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsTestAppointmentBL()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

                
        public clsTestAppointmentBL(int TestAppointmentID, clsTestType.enTestType TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID= TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplicationsBuisnessLayer.FindBaseApplication(RetakeTestApplicationID);

            Mode = enMode.Update;
        }



        //public static List<TestAppointment> GetApplicationTestAppointmentsPerTestType(int localDrivingLicenseApplicationID, int testTypeID)
        //{
        //    // هنا أي Business Rules مستقبلًا تتحط
        //    return clsTestAppointmentDA.GetApplicationTestAppointmentsPerTestType(localDrivingLicenseApplicationID, testTypeID);
        //}


        public static DataTable GetApplicationTestAppointmentsPerTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestAppointmentDA.GetApplicationTestAppointmentsPerTestType(localDrivingLicenseApplicationID, testTypeID);
        }
                
        public static clsTestAppointmentBL Find( int TestAppointmentID)
        {

            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;


            if (clsTestAppointmentDA.GetTestAppointmentInfoByID(TestAppointmentID , ref TestTypeID , ref LocalDrivingLicenseApplicationID,
               ref AppointmentDate , ref PaidFees , ref CreatedByUserID , ref IsLocked , ref RetakeTestApplicationID ))

            return new clsTestAppointmentBL(TestAppointmentID, (clsTestType.enTestType) TestTypeID , LocalDrivingLicenseApplicationID, 
                AppointmentDate , PaidFees , CreatedByUserID , IsLocked , RetakeTestApplicationID );  

            else
            return null;
        }



        public static clsTestAppointmentBL GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDA.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointmentBL(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }



        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentDA.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }



        private bool _UpdateTestAppointment()
        {

            return clsTestAppointmentDA.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();
            }

            return false;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentDA.GetTestID(TestAppointmentID);
        }



    }
}
