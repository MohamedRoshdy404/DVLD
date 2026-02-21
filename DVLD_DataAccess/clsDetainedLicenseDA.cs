using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseDA
    {








        public static bool GetDetainedLicenseByLicenseID(
    int LicenseID,
    ref int DetainID,
    ref DateTime DetainDate,
    ref decimal FineFees,
    ref int CreatedByUserID,
    ref bool IsReleased,
    ref DateTime ReleaseDate,
    ref int ReleasedByUserID,
    ref int ReleaseApplicationID)
        {
            DetainID = -1;
            DetainDate = DateTime.MinValue;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;

            bool isFound = false;

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = @"SELECT *
                         FROM DetainedLicenses
                         WHERE LicenseID = @LicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            DetainID = (int)reader["DetainID"];
                            DetainDate = (DateTime)reader["DetainDate"];
                            FineFees = (decimal)reader["FineFees"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            IsReleased = (bool)reader["IsReleased"];

                            if (reader["ReleaseDate"] != DBNull.Value)
                                ReleaseDate = (DateTime)reader["ReleaseDate"];

                            if (reader["ReleasedByUserID"] != DBNull.Value)
                                ReleasedByUserID = (int)reader["ReleasedByUserID"];

                            if (reader["ReleaseApplicationID"] != DBNull.Value)
                                ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                        }
                    }
                }
            }

            return isFound;
        }



        public static int AddNewDetainedLicense(
            int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
                {
                    string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID,
                                DetainDate,
                                FineFees,
                                CreatedByUserID,
                                IsReleased)
                             VALUES
                               (@LicenseID,
                                @DetainDate, 
                                @FineFees, 
                                @CreatedByUserID,
                                0);

                             SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        command.Parameters.AddWithValue("@FineFees", FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            DetainID = insertedID;
                    }
                }
            }
            catch
            {
                // Log error if needed
            }

            return DetainID;
        }




        public static bool UpdateDetainedLicense(
    int DetainID,
    int LicenseID,
    DateTime DetainDate,
    decimal FineFees,
    int CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
                {
                    string query = @"UPDATE dbo.DetainedLicenses
                             SET LicenseID = @LicenseID,
                                 DetainDate = @DetainDate,
                                 FineFees = @FineFees,
                                 CreatedByUserID = @CreatedByUserID                
                             WHERE DetainID = @DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", DetainID);
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        command.Parameters.AddWithValue("@FineFees", FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Log error if needed
            }

            return rowsAffected > 0;
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
                {
                    string query = @"SELECT 1
                             FROM detainedLicenses
                             WHERE LicenseID = @LicenseID
                             AND IsReleased = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                            IsDetained = true;
                    }
                }
            }
            catch
            {
                // Log error if needed
            }

            return IsDetained;
        }




    }
}
