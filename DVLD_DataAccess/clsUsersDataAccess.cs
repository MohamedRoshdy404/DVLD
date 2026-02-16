using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUsersDataAccess
    {
        
        public static DataTable GetInfoUsers()
        {
            DataTable dtUsers = new DataTable();



            SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString);
            string query = "SELECT Users.UserID,  People.PersonID,  People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName as FullName,Users.UserName, Users.IsActive FROM   People INNER JOIN  Users ON People.PersonID = Users.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtUsers.Load(reader);
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





            return dtUsers;
        }





        public static bool FindUserByUserNameAndPassword(ref int UserID, ref int PersonID, string UserName, string Password, ref byte IsActive , ref int Permissions)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = "select UserID , PersonID , UserName , Password , IsActive , Permissions from Users where UserName = @UserName and Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                UserName = reader["UserName"]?.ToString() ?? "";
                                Password = reader["Password"]?.ToString() ?? "";
                                IsActive = Convert.ToByte(reader["IsActive"]);
                                Permissions = Convert.ToInt32(reader["Permissions"]);


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return isFound;
        }


        public static int AddNewUser(int PersonID , string UserName , string Password , short IsActive)
        {

            int UserID = -1;



            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "INSERT INTO Users (PersonID , UserName , Password , IsActive) values (@PersonID , @UserName , @Password , @IsActive ) SELECT SCOPE_IDENTITY()";

               using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("PersonID" , PersonID);
                    command.Parameters.AddWithValue("UserName", UserName);
                    command.Parameters.AddWithValue("Password", Password);
                    command.Parameters.AddWithValue("IsActive", IsActive);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            UserID = insertedID;
                        }
                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return UserID;

        }


               
        public static bool UpdateUser( int UserID,  string UserName , string Password , byte IsActive)
        {

            int rowAffected = 0;



            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "UPDATE Users SET UserName = @UserName, Password = @Password, IsActive = @IsActive WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    try
                    {
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();

                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return (rowAffected > 0);

        }
               
        public static bool ChangePassword( int UserID,  string Password )
        {

            int rowAffected = 0;



            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "UPDATE Users SET Password = @Password WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Password", Password);

                    try
                    {
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();

                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return (rowAffected > 0);

        }




                       
        public static bool FindUser(int UserID, ref int PersonID ,ref string UserName , ref string Password ,ref byte IsActive , ref int Permissions)
        {

            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "select PersonID , UserName , Password , IsActive , Permissions  from Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("UserID", UserID);


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                               
                                PersonID = Convert.ToInt32(reader["PersonID"]);
                                UserName = reader["UserName"].ToString();
                                Password = reader["Password"].ToString();
                                IsActive = Convert.ToByte(reader["IsActive"]);
                                Permissions = Convert.ToInt32(reader["Permissions"]);


                            }
                            else
                            {
                               // Console.WriteLine("No data found for the given UserID.");
                            }


                        }

                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return IsFound;

        }



                               
        public static bool isExist(int PersonID)
        {

            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "select isExist = 1  from Users WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("PersonID", PersonID);


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            IsFound = reader.HasRows;

                        }

                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return IsFound;

        }



               
        public static bool DeleteUser(int UserID)
        {

            int rosAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {

                string query = "Delete from Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("UserID", UserID);


                    try
                    {
                        connection.Open();

                        rosAffected = command.ExecuteNonQuery();

                    }
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close ();
                    }

                }



            }

            return (rosAffected > 0);

        }






    }
}
