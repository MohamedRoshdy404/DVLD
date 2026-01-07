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





        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestDA.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }






    }
}
