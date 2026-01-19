using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestBL
    {





        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointmentBL TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTestBL()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsTestBL(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointmentBL.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }



        public static clsTestBL Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestDA.GetTestInfoByID(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTestBL(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        //public static clsTestBL Find(int TestID)
        //{

        //    int TestAppointmentID = -1;
        //    bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

        //    if (clsTestDA.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))

        //        return new clsTestBL(TestID,
        //              TestAppointmentID, TestResult,
        //              Notes, CreatedByUserID);
        //    else
        //        return null;
        //}

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestDA.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }


        public static clsTestBL FindLastTestPerPersonAndLicenseClass
    (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestDA.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTestBL(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }



        public bool _AddNewTest()
        {
            this.TestID = clsTestDA.AddNewTest(this.TestAppointmentID , this.TestResult , this.Notes , this.CreatedByUserID);
            return this.TestID != -1;
        }

        public bool _UpdateTest()
        {
             return clsTestDA.UpdateTest(this.TestID, this.TestAppointmentID , this.TestResult , this.Notes , this.CreatedByUserID);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();
            }

            return false;
        }

    }
}
