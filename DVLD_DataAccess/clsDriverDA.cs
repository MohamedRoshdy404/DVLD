using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDriverDA
    {




        public static int AddNewDriver(
    int PersonID,
    int CreatedByUserID)
        {
            int DriverID = -1;

            string query = @"INSERT INTO Drivers
                     (PersonID,
                      CreatedByUserID)
                     VALUES
                     (@PersonID,
                      @CreatedByUserID);

                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DriverID = insertedID;
                        }
                    }
                    catch
                    {
                        DriverID = -1;
                    }
                }
            }

            return DriverID;
        }










    }
}
