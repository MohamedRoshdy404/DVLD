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



        public static clsLicenseClassBuisnessLayer FindLicenseClassesByID(int LicenseClassID)
        {

            string className = string.Empty , ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0,  DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassDataAccess.GetLicenseClassInfoByID(LicenseClassID, ref className, ref  ClassDescription, ref   MinimumAllowedAge, ref  DefaultValidityLength, ref  ClassFees))
            {
                return new clsLicenseClassBuisnessLayer(className, LicenseClassID , ClassDescription , MinimumAllowedAge, DefaultValidityLength,  ClassFees);
            }
            else
            {
                return null;
            }


        }






    }
}
