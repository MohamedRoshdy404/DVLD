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






        public static int AddNewLicense(
    int ApplicationID,
    int DriverID,
    int LicenseClass,
    DateTime IssueDate,
    DateTime ExpirationDate,
    string Notes,
    decimal PaidFees,
    bool IsActive,
    byte IssueReason,
    int CreatedByUserID)
        {
            int LicenseID = -1;

            string query = @"INSERT INTO Licenses
                     (ApplicationID,
                      DriverID,
                      LicenseClass,
                      IssueDate,
                      ExpirationDate,
                      Notes,
                      PaidFees,
                      IsActive,
                      IssueReason,
                      CreatedByUserID)
                     VALUES
                     (@ApplicationID,
                      @DriverID,
                      @LicenseClass,
                      @IssueDate,
                      @ExpirationDate,
                      @Notes,
                      @PaidFees,
                      @IsActive,
                      @IssueReason,
                      @CreatedByUserID);

                      UPDATE Applications 
                      SET ApplicationStatus = 3 WHERE ApplicationID = @ApplicationID;

                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                    if (string.IsNullOrEmpty(Notes))
                        command.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Notes", Notes);

                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@IssueReason", IssueReason);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
