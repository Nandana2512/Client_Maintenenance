using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_Maintenance.BLL;
using System.Data.SqlClient;
using Client_Maintenance.DAL;

namespace Client_Maintenance.DAL
{
    public static class ClientDB
    {
        public static void SaveRecord(Clients clients)
        {
            
            SqlConnection conn = UtilityDB.GetDBConnection();

           
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Clients (ClientNumber,LastName,FirstName,PhoneNumber,Email) " +
                                    "VALUES(@ClientNumber,@LastName,@FirstName,@PhoneNumber,@Email)";
            cmdInsert.Parameters.AddWithValue("@ClientNumber", clients.ClientNumber);
            
            cmdInsert.Parameters.AddWithValue("@LastName", clients.LastName);
            cmdInsert.Parameters.AddWithValue("@FirstName", clients.FirstName);
            cmdInsert.Parameters.AddWithValue("@PhoneNumber", clients.PhoneNumber);
            cmdInsert.Parameters.AddWithValue("@Email", clients.Email);
            cmdInsert.ExecuteNonQuery();
            
            conn.Close();
        }

        public static List<Clients> GetAllRecords()
        {
            List<Clients> listC = new List<Clients>();
            SqlConnection conn = UtilityDB.GetDBConnection();
           
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Clients", conn);
           
            SqlDataReader reader = cmdSelectAll.ExecuteReader(); 
            Clients cli;
            while (reader.Read())
            {
                cli = new Clients();
                cli.ClientNumber = Convert.ToInt32(reader["ClientNumber"]);
                cli.LastName = reader["LastName"].ToString();
                cli.FirstName = reader["FirstName"].ToString();               
                cli.PhoneNumber = reader["PhoneNumber"].ToString();
                cli.Email = reader["Email"].ToString();
                listC.Add(cli);

            }
            conn.Close();
            return listC;
        }

        public static Clients SearchRecord(int cNum)
        {
            Clients cli = new Clients();

            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchById = new SqlCommand();
            cmdSearchById.Connection = conn;
            cmdSearchById.CommandText = "SELECT * FROM Clients " +
                                        "WHERE ClientNumber=@ClientNumber";

            cmdSearchById.Parameters.AddWithValue("@ClientNumber", cNum);
            SqlDataReader reader = cmdSearchById.ExecuteReader();
            if (reader.Read()) 
            {
                cli.ClientNumber = Convert.ToInt32(reader["ClientNumber"]);
                cli.LastName = reader["LastName"].ToString().Trim();
                cli.FirstName = reader["FirstName"].ToString();
                cli.PhoneNumber = reader["PhoneNumber"].ToString();
                cli.Email = reader["Email"].ToString();
            }

            else  
            {
                cli = null;
            }
            conn.Close();
            return cli;

        }
        public static List<Clients> SearchRecord(string input) 
        {
            List<Clients> listC = new List<Clients>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByName = new SqlCommand();
            cmdSearchByName.Connection = conn;
            cmdSearchByName.CommandText = "SELECT * FROM Clients " +
                                          "WHERE FirstName = @FirstName "; 
                                          
            cmdSearchByName.Parameters.AddWithValue("@FirstName", input);
            
            SqlDataReader reader = cmdSearchByName.ExecuteReader(); 
            Clients cli;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cli = new Clients();
                    cli.ClientNumber = Convert.ToInt32(reader["ClientNumber"]);
                    cli.LastName = reader["LastName"].ToString();
                    cli.FirstName = reader["FirstName"].ToString();
                    cli.PhoneNumber = reader["PhoneNumber"].ToString();
                    cli.Email = reader["Email"].ToString();
                    listC.Add(cli);
                }

            }
            conn.Close();
            return listC;

        }

        public static bool IsUniqueClientNumber(int cNum)
        {
            Clients cli = SearchRecord(cNum);
            if (cli != null)
            {
                return false;
            }
            return true;
        }




    }
}
