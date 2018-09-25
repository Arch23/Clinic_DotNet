using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Clinica.Models
{
    public class ConnectionDB
    {
        private static ConnectionDB INSTANCE;
        private MySqlConnection connection;
        private MySqlCommand command;

        private ConnectionDB()
        {
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
        }

        public static ConnectionDB GetInstance()
        {
            return INSTANCE ?? (INSTANCE = new ConnectionDB());
        }

        public string Execute(string pSQL)
        {
            string result = "";
            int changedRows = 0;
            try
            {
                connection.Open();
                command = new MySqlCommand(pSQL, connection);

                changedRows = command.ExecuteNonQuery();

                if (changedRows == 0)
                    result = "No rows changed.";
                else
                    result = "";
               
            }
            catch(Exception ex)
            {
                result = $"Error with processing the SQL: '{pSQL}'\r\nException Message: {ex.Message}";
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public DataTable ExecuteQuery(string pSQL, out string result)
        {
            DataTable reader = new DataTable();
            result = "";

            try
            {
                connection.Open();
                command = new MySqlCommand(pSQL, connection);

                reader.Load(command.ExecuteReader());
                result = "";
            }
            catch (Exception ex)
            {
                result = $"Error with processing the SQL: '{pSQL}'\r\nException Message: {ex.Message}";
            }
            finally
            {
                connection.Close();
            }

            return reader;
        }
    }
}