using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationTypeBuisnessLayer
    {



        public int ApplicationTypeID {  get; set; }
        public string ApplicationTypeTitle {  get; set; }
        public decimal ApplicationFees {  get; set; }



        public clsApplicationTypeBuisnessLayer()
        {
            this.ApplicationTypeID = 0;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0;

        }
        
        private clsApplicationTypeBuisnessLayer(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID ;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;

        }

        public static DataTable GetAllApplicationType()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationType();
        }

        public static clsApplicationTypeBuisnessLayer Find(int ID)
        {
            string ApplicationTypeTitle = ""; decimal ApplicationFees = 0;
            if (clsApplicationTypeDataAccess.GetApplicationTypeInfoByID(ID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypeBuisnessLayer (ID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }


        }



        public bool UpdateApplicationType()
        {

            if (clsApplicationTypeDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle , this.ApplicationFees))
            
                return true;
            else
                return false;
        }





    }









}
