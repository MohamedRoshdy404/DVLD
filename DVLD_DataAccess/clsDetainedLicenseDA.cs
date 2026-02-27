using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseDA
    {



        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
                {
                    string query = @"SELECT *
                             FROM detainedLicenses_View
                             ORDER BY IsReleased, DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
            }
            catch
            {
                // Log if needed
            }

            return dt;
        }

        public static bool GetDetainedLicenseInfoByLicenseID(
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
            bool isFound = false;

            string query = @"SELECT TOP 1 *
                     FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID
                     ORDER BY DetainID DESC";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                DetainID = (int)reader["DetainID"];
                                DetainDate = (DateTime)reader["DetainDate"];
                                FineFees = Convert.ToDecimal(reader["FineFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];

                                IsReleased = (bool)reader["IsReleased"];

                                ReleaseDate = reader["ReleaseDate"] == DBNull.Value
                                              ? DateTime.MaxValue
                                              : (DateTime)reader["ReleaseDate"];

                                ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value
                                                   ? -1
                                                   : (int)reader["ReleasedByUserID"];

                                ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value
                                                       ? -1
                                                       : (int)reader["ReleaseApplicationID"];
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
            }

            return IsDetained;
        }


        public static bool ReleaseDetainedLicense(
            int DetainID,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE DetainedLicenses
                     SET IsReleased = 1,
                         ReleaseDate = @ReleaseDate, 
                         ReleasedByUserID = @ReleasedByUserID,
                         ReleaseApplicationID = @ReleaseApplicationID
                     WHERE DetainID = @DetainID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }


    }
}
