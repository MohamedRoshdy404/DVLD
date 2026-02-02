using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseDA
    {


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
