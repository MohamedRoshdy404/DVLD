using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTestTypeBuisnessLayer
    {

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypeBuisnessLayer()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = string.Empty;
            this.TestTypeDescription = string.Empty;
            this.TestTypeFees = 0;

            Mode = enMode.AddNew;
        }

     
        private clsTestTypeBuisnessLayer(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }







        public static DataTable GetAllInfoTestType()
        {
            return clsTestTypeDataAccess.GetAllInfoTestType();
        }




        public static clsTestTypeBuisnessLayer Find(int TestTypeID)
        {

          
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = 0;


            if (clsTestTypeDataAccess.FindTestType(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestTypeBuisnessLayer(TestTypeID , TestTypeTitle , TestTypeDescription , TestTypeFees);
            }
            else
            {
                return null;
            }


        }



        private bool _UpdateTestType()
        {

            if (clsTestTypeDataAccess.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees))
                return true;

            else
                return false;
        }




        public bool Save()
        {

            switch (Mode)
            {

                case enMode.Update:

                    return _UpdateTestType();



            }


            return false;

        }



    }
}
