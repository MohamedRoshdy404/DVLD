using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicenseClassBuisnessLayer
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID {  get; set; }
        public string ClassName {  get; set; }
        public string ClassDescription {  get; set; }
        public byte MinimumAllowedAge {  get; set; }
        public byte DefaultValidityLength {  get; set; }
        public decimal ClassFees {  get; set; }



        public clsLicenseClassBuisnessLayer()
        {
            this.LicenseClassID = 0;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            Mode = enMode.AddNew;
        }

        public clsLicenseClassBuisnessLayer(string ClassName,  int LicenseClassID, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength,  decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription =ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }



        
        public clsLicenseClassBuisnessLayer(int LicenseClassID, string ClassName , string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength,  decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription =ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassDataAccess.GetAllLicenseClasses();
        }



        public static clsLicenseClassBuisnessLayer FindLicenseClassesByClassName(string ClassName)
        {

            int LicenseClassID = 0;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0,  DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.FindLicenseClassesByClassName(ClassName , ref LicenseClassID, ref  ClassDescription, ref   MinimumAllowedAge, ref  DefaultValidityLength, ref  ClassFees))
            {
                return new clsLicenseClassBuisnessLayer(ClassName , LicenseClassID , ClassDescription , MinimumAllowedAge, DefaultValidityLength,  ClassFees);
            }
            else
            {
                return null;
            }


        }



        //public static clsLicenseClassBuisnessLayer FindLicenseClassesByID(int LicenseClassID)
        //{

        //    string className = string.Empty , ClassDescription = string.Empty;
        //    byte MinimumAllowedAge = 0,  DefaultValidityLength = 0;
        //    decimal ClassFees = 0;

        //    if (clsLicenseClassDataAccess.GetLicenseClassInfoByID(LicenseClassID, ref className, ref  ClassDescription, ref   MinimumAllowedAge, ref  DefaultValidityLength, ref  ClassFees))
        //    {
        //        return new clsLicenseClassBuisnessLayer(className, LicenseClassID , ClassDescription , MinimumAllowedAge, DefaultValidityLength,  ClassFees);
        //    }
        //    else
        //    {
        //        return null;
        //    }


        //}



        private bool _AddNewLicenseClass()
        {
            //call DataAccess Layer 

            this.LicenseClassID = clsLicenseClassDataAccess.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);


            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            //call DataAccess Layer 

            return clsLicenseClassDataAccess.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public static clsLicenseClassBuisnessLayer Find(int LicenseClassID)
        {
            string ClassName = ""; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClassBuisnessLayer(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }

        public static clsLicenseClassBuisnessLayer Find(string ClassName)
        {
            int LicenseClassID = -1; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClassBuisnessLayer(ClassName, LicenseClassID, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }

            return false;
        }









    }
}
