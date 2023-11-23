using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Proyecto
{
    public class serverLogic
    {
        public bool login(string id, string password, string pathDB, string[] receivedData)
        {

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathDB + ";Version=3;"))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE IDNumber = @idnumber AND hashPassword = @password";

                /* using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                    inputPassword = BitConverter.ToString(data).Replace("-", string.Empty);
                }*/

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idnumber", id);
                    command.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userType = reader["userType"].ToString();
                            receivedData[0] = userType;
                            receivedData[1] = reader["userID"].ToString();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}