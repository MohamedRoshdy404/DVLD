using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using DVLD_Models;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentDA
    {


        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }



        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
            ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

                using (SqlCommand command = new SqlCommand(query , connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                    connection.Open();

                    try
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                TestTypeID = (int)reader["TestTypeID"];
                                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                                IsLocked = (bool)reader["IsLocked"];

                                if (reader["RetakeTestApplicationID"] == DBNull.Value)
                                    RetakeTestApplicationID = -1;
                                else
                                    RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


                            }
                            else
                            {
                                isFound &= false;   
                            }
                        }


                }
                    catch (Exception ex)
                    {

                    }


                }

            }


            return isFound;
        }



        public static bool GetLastTestAppointment(
                        int LocalDrivingLicenseApplicationID,
                        int TestTypeID,
                        ref int TestAppointmentID,
                        ref DateTime AppointmentDate,
                        ref decimal PaidFees,
                        ref int CreatedByUserID,
                        ref bool IsLocked,
                        ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            string query = @"SELECT TOP 1 *
                     FROM TestAppointments
                     WHERE TestTypeID = @TestTypeID
                     AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                     ORDER BY TestAppointmentID DESC";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                TestAppointmentID = (int)reader["TestAppointmentID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsLocked = (bool)reader["IsLocked"];

                                RetakeTestApplicationID =
                                    reader["RetakeTestApplicationID"] == DBNull.Value
                                    ? -1
                                    : (int)reader["RetakeTestApplicationID"];
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




        //public static List<TestAppointment> GetApplicationTestAppointmentsPerTestType(int localDrivingLicenseApplicationID, int testTypeID)
        //{
        //    List<TestAppointment> list = new List<TestAppointment>();

        //    using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
        //    {
        //        string query = @"SELECT TestAppointmentID,
        //                                AppointmentDate,
        //                                PaidFees,
        //                                IsLocked
        //                         FROM TestAppointments
        //                         WHERE TestTypeID = @TestTypeID
        //                         AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
        //                         ORDER BY TestAppointmentID DESC;";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
        //            command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

        //            connection.Open();

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(new TestAppointment
        //                    {
        //                        TestAppointmentID = reader.GetInt32(0),
        //                        AppointmentDate = reader.GetDateTime(1),
        //                        PaidFees = reader.GetDecimal(2),
        //                        IsLocked = reader.GetBoolean(3)
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return list;
        //}


        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static int AddNewTestAppointment(
                    int TestTypeID,
                    int LocalDrivingLicenseApplicationID,
                    DateTime AppointmentDate,
                    decimal PaidFees,
                    int CreatedByUserID,
                    int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;


            string query = @"INSERT INTO TestAppointments
                     (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                      PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                     VALUES
                     (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate,
                      @PaidFees, @CreatedByUserID, 0, @RetakeTestApplicationID);

                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    if (RetakeTestApplicationID == -1)
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestAppointmentID = insertedID;
                        }
                    }
                    catch
                    {
                        TestAppointmentID = -1;
                    }
                }
            }

            return TestAppointmentID;
        }






        public static bool UpdateTestAppointment(
                    int TestAppointmentID,
                    int TestTypeID,
                    int LocalDrivingLicenseApplicationID,
                    DateTime AppointmentDate,
                    decimal PaidFees,
                    int CreatedByUserID,
                    bool IsLocked,
                    int RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE TestAppointments
                     SET TestTypeID = @TestTypeID,
                         LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                         AppointmentDate = @AppointmentDate,
                         PaidFees = @PaidFees,
                         CreatedByUserID = @CreatedByUserID,
                         IsLocked = @IsLocked,
                         RetakeTestApplicationID = @RetakeTestApplicationID
                     WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsLocked", IsLocked);

                    if (RetakeTestApplicationID == -1)
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

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



        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;

            string query = @"SELECT TestID
                     FROM Tests
                     WHERE TestAppointmentID = @TestAppointmentID;";

            using (SqlConnection connection =
                   new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestID = insertedID;
                        }
                    }
                    catch
                    {
                        TestID = -1;
                    }
                }
            }

            return TestID;
        }






    }
}
