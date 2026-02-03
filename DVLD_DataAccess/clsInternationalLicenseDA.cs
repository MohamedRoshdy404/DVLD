using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseDA
    {
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"
                        SELECT
                            InternationalLicenseID,
                            ApplicationID,
                            DriverID,
                            IssuedUsingLocalLicenseID,
                            IssueDate,
                            ExpirationDate,
                            IsActive
                        FROM InternationalLicenses;
                    ";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
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


        public static bool GetActiveInternationalLicenseByDriverID(
                int DriverID,
                ref int InternationalLicenseID,
                ref int ApplicationID,
                ref int IssuedUsingLocalLicenseID,
                ref DateTime IssueDate,
                ref DateTime ExpirationDate,
                ref int CreatedByUserID)
        {
            bool isFound = false;

            string query = @"
                        SELECT TOP 1
                            InternationalLicenseID,
                            ApplicationID,
                            IssuedUsingLocalLicenseID,
                            IssueDate,
                            ExpirationDate,
                            CreatedByUserID
                        FROM InternationalLicenses
                        WHERE DriverID = @DriverID
                          AND IsActive = 1;
                    ";

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

                                InternationalLicenseID = (int)reader["InternationalLicenseID"];
                                ApplicationID = (int)reader["ApplicationID"];
                                IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
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





        public static int AddInternationalLicense(
                int ApplicationID,
                int DriverID,
                int IssuedUsingLocalLicenseID,
                DateTime IssueDate,
                DateTime ExpirationDate,
                bool IsActive,
                int CreatedByUserID
                 )
        {

            int InternationalLicenseID = -1;

            string query = @"
                        INSERT INTO InternationalLicenses
                        (
                            ApplicationID,
                            DriverID,
                            IssuedUsingLocalLicenseID,
                            IssueDate,
                            ExpirationDate,
                            IsActive,
                            CreatedByUserID
                        )
                        VALUES
                        (
                            @ApplicationID,
                            @DriverID,
                            @IssuedUsingLocalLicenseID,
                            @IssueDate,
                            @ExpirationDate,
                            @IsActive,
                            @CreatedByUserID
                        );

                        SELECT SCOPE_IDENTITY();
                    ";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            InternationalLicenseID = insertedID;
                        }
                    }
                    catch
                    {
                        InternationalLicenseID = -1;
                    }
                }
            }

            return InternationalLicenseID;
        }



    }
}
