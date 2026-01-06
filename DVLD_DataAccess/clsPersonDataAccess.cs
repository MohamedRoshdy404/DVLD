using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsPersonDataAccess
    {

        public static DataTable GetAllPeople()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {


                string query = @"SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName,         People.ThirdName, People.LastName, 

                        case 
                        when Gender = 0 then 'Male'
                        else 'Female'
                        end as Gender,
                        People.DateOfBirth, Countries.CountryName, People.Phone, People.Email
                        FROM            Countries INNER JOIN
                         People ON Countries.CountryID = People.NationalityCountryID ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }


            }

            return dataTable;

        }



        public static int AddNewPerson(string NationalNo , string FirstName , string SecondName , string ThirdName  , string LastName , DateTime DateOfBirth , short Gender , string Address , string Phone , string Email , int NationalityCountryID , string ImagePath)
        {

            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = @"   
                        INSERT INTO People (NationalNo , FirstName , SecondName , ThirdName , LastName , DateOfBirth , Gender , Address , Phone , Email , NationalityCountryID , ImagePath)
			                        VALUES (@NationalNo , @FirstName , @SecondName , @ThirdName , @LastName , @DateOfBirth ,@Gender ,@Address , @Phone , @Email , @NationalityCountryID , @ImagePath   ) ;
                                    SELECT SCOPE_IDENTITY();
                                    ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@NationalNo", string.IsNullOrEmpty(NationalNo) ? (object)DBNull.Value : NationalNo);
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(FirstName) ? (object)DBNull.Value : FirstName);
                    command.Parameters.AddWithValue("@SecondName", string.IsNullOrEmpty(SecondName) ? (object)DBNull.Value : SecondName);
                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(LastName) ? (object)DBNull.Value : LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth == null ? (object)DBNull.Value : DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address) ? (object)DBNull.Value : Address);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(Phone) ? (object)DBNull.Value : Phone);

                    if (Email != null)
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

                    
                    if (ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PersonID = insertedID;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }


            }

            return PersonID;

        }




        public static bool UpdatePerson(int PersonID,  string NationalNo,  string FirstName,  string SecondName,  string ThirdName,  string LastName,  DateTime DateOfBirth,  short Gender,  string Address,  string Phone,  string Email,  int NationalityCountryID,  string ImagePath)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = @"
                                    UPDATE People
                                    SET 
                                        NationalNo = @NationalNo,
                                        FirstName = @FirstName,
                                        SecondName = @SecondName,
                                        ThirdName = @ThirdName,
                                        LastName = @LastName,
                                        DateOfBirth = @DateOfBirth,
                                        Gender = @Gender,
                                        Address = @Address,
                                        Phone = @Phone,
                                        Email = @Email,
                                        NationalityCountryID = @NationalityCountryID,
                                        ImagePath = @ImagePath
                                    WHERE PersonID = @PersonID;
                                    ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);

                    if (Email != null)
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


                    if (ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }


            }

            return (rowsAffected > 0);

        }








        public static bool Find(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = @"
                                    SELECT 
                                        NationalNo,
                                        FirstName,
                                        SecondName,
                                        ThirdName,
                                        LastName,
                                        DateOfBirth,
                                        Gender,
                                        Address,
                                        Phone,
                                        Email,
                                        NationalityCountryID,
                                        ImagePath
                                        FROM People
                                        WHERE PersonID = @PersonID";

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
                                // قراءة البيانات من قاعدة البيانات
                                NationalNo = reader["NationalNo"].ToString();
                                FirstName = reader["FirstName"].ToString();
                                SecondName = reader["SecondName"].ToString();
                                ThirdName = reader["ThirdName"].ToString();
                                LastName = reader["LastName"].ToString();
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                Gender = Convert.ToByte(reader["Gender"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"].ToString();
                                NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                                ImagePath = reader["ImagePath"].ToString();

                            }
                            else
                            {
                                // إذا لم يتم العثور على البيانات
                                Console.WriteLine("No data found for the given PersonID.");
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



        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString);

            string query = @"Delete People 
                                             where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                 Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }



        public static bool Find(string NationalNo, ref int PersonID , ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsSettingsConnectoinStrinng.connectionString))
            {
                string query = @"
                                    SELECT 
                                        PersonID,
                                        FirstName,
                                        SecondName,
                                        ThirdName,
                                        LastName,
                                        DateOfBirth,
                                        Gender,
                                        Address,
                                        Phone,
                                        Email,
                                        NationalityCountryID,
                                        ImagePath
                                        FROM People
                                        WHERE NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    try
                    {

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                // قراءة البيانات من قاعدة البيانات
                                PersonID = Convert.ToInt32( reader["PersonID"]);
                                FirstName = reader["FirstName"].ToString();
                                SecondName = reader["SecondName"].ToString();
                                ThirdName = reader["ThirdName"].ToString();
                                LastName = reader["LastName"].ToString();
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                Gender = Convert.ToByte(reader["Gender"]);
                                Address = reader["Address"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"].ToString();
                                NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                                ImagePath = reader["ImagePath"].ToString();

                            }
                            else
                            {
                                // إذا لم يتم العثور على البيانات
                                Console.WriteLine("No data found for the given PersonID.");
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














    }
}
