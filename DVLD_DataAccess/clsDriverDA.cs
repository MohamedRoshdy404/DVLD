using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccess
{
    public class clsDriverDA
    {



        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT Licenses.LicenseID,
                            Licenses.ApplicationID,
                            LicenseClasses.ClassName,
                            Licenses.IssueDate,
                            Licenses.ExpirationDate,
                            Licenses.IsActive
                     FROM Licenses
                     INNER JOIN LicenseClasses
                         ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                     WHERE DriverID = @DriverID
                     ORDER BY IsActive DESC, ExpirationDate DESC";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                        dt = new DataTable();
                    }
                }
            }

            return dt;
        }



        public static DataTable GetAllDriverLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"
                        SELECT 
                            Drivers.DriverID,
                            People.PersonID,
                            People.NationalNo,
                            People.FirstName + ' ' + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName AS FullName,
                            Drivers.CreatedDate,
                            MAX(CAST(Licenses.IsActive AS INT)) AS ActiveLicense
                        FROM Drivers
                        INNER JOIN People ON Drivers.PersonID = People.PersonID
                        INNER JOIN Licenses ON Drivers.DriverID = Licenses.DriverID
                        GROUP BY 
                            People.PersonID,
                            Drivers.DriverID,
                            People.NationalNo,
                            People.FirstName,
                            People.SecondName,
                            People.ThirdName,
                            People.LastName,
                            Drivers.CreatedDate;
                    ";

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch
                    {
                        dt = null;
                    }
                }
            }

            return dt;
        }
    




    public static bool GetDriverInfoByDriverID(
                    int DriverID,
                    ref int PersonID,
                    ref int CreatedByUserID,
                    ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = @"SELECT PersonID, CreatedByUserID, CreatedDate
                     FROM Drivers
                     WHERE DriverID = @DriverID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                            }
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }




        public static bool GetDriverInfoByPersonID(
                int PersonID,
                ref int DriverID,
                ref int CreatedByUserID,
                ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = @"SELECT DriverID, CreatedByUserID, CreatedDate
                     FROM Drivers
                     WHERE PersonID = @PersonID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                DriverID = (int)reader["DriverID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                            }
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }



        public static int GetDriverIDByPersonID(int PersonID)
        {
            int DriverID = -1;

            string query = @"SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                            DriverID = Convert.ToInt32(result);
                    }
                    catch
                    {
                        DriverID = -1;
                    }
                }
            }

            return DriverID;
        }


        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Drivers WHERE PersonID = @PersonID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.Read();
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }



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
