using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestTypeDA
    {



        public static DataTable GetAllInfoTestType()
        {
            DataTable dtTestType = new DataTable();

            SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString);

            string query = "select * from TestTypes";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtTestType.Load(reader);
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return dtTestType;
        }



        public static bool FindTestType(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestTypeFees)
        {

            bool isFound = false;


            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "select TestTypeID , TestTypeTitle , TestTypeDescription , TestTypeFees from TestTypes where TestTypeID = @TestTypeID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                TestTypeID = (int)reader["TestTypeID"];
                                TestTypeTitle = (string)reader["TestTypeTitle"];
                                TestTypeDescription = (string) reader["TestTypeDescription"]?.ToString() ?? "";
                                TestTypeFees = (decimal)reader["TestTypeFees"];


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }


                }





            }







            return isFound;
        }



        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle , string TestTypeDescription, decimal TestTypeFees)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = "UPDATE TestTypes SET TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription , TestTypeFees = @TestTypeFees  WHERE TestTypeID = @TestTypeID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);



                    try
                    {
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }


                }
            }

            return (rowAffected > 0);
        }
















    }
}
