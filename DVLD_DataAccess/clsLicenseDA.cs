using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseDA
    {



        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            string query = @"SELECT Licenses.LicenseID
                     FROM Licenses
                     INNER JOIN Drivers
                         ON Licenses.DriverID = Drivers.DriverID
                     WHERE Licenses.LicenseClass = @LicenseClass
                       AND Drivers.PersonID = @PersonID
                       AND IsActive = 1;";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseID = insertedID;
                        }
                    }
                    catch
                    {
                        LicenseID = -1;
                    }
                }
            }

            return LicenseID;
        }




    }

}
