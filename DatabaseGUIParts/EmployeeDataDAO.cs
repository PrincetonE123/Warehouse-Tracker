using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.VisualBasic.ApplicationServices;


namespace DatabaseGUIParts 
{
    public class EmployeeDataDAO
    {
        //Connection string to the Database, this is a local server, so only Server and Initial Catalog need to be changed based on the user
        string connectionString = "Server=NEURALYNX;Initial Catalog=WHMngmntSystem;Integrated Security=True;";


        public int getPermissionLevel(string username)
        {
           username = SharedData.currentUser; // sets parameter to public variable currentUser
            
            int permissionLvl = 0;  // automatically set permLvl to 0

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    //Query the permLevel from database
                    string query = "SELECT PermissionLevel FROM dbo.Employees WHERE Username = @username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        permissionLvl = Convert.ToInt32(result);
                    }
                    else 
                    {
                        Console.WriteLine($"Error: No user found with username '{username}'.");
                        permissionLvl = -1; // Special value indicating "user not found"
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
            }
            SharedData.permissionLevel = permissionLvl;
            return SharedData.permissionLevel;
        }


        public int getEmployeeID(string username)
        {
            username = SharedData.currentUser; // sets parameter to public variable currentUser

            int empID = -1;  //

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    //Query the permLevel from database
                    string query = "SELECT EmployeeID FROM dbo.Employees WHERE Username = @username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        empID = Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine($"Error: No user found with username '{username}'.");
                        empID = -1; // Special value indicating "user not found"
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
            }
            SharedData.employeeID = empID;
            return SharedData.employeeID;
        }


        // Method to verify user credentials
        public bool ValidateUserCredentials(string username, string password)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedHashedPassword = "";
                try
                {
                    connection.Open();

                    //TODO: Change based on Table name and Column names for User and Pass IDs
                    string query = "SELECT * FROM dbo.Employees WHERE Username = @username";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@username", username);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                //This gets the password stored in the DB if Null("??") then it equals ""
                                storedHashedPassword = reader["Password"].ToString() ?? "";
                            }
                            else
                            {
                                Console.WriteLine("Username not found.");
                                return false;
                            }
                        }
                    }
                    return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return false;
        } //end of validation
    }
}
