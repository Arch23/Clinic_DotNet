using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Clinica.Models
{
    [Serializable]
    public class User
    {
        private static readonly string FILE_NAME = "User";

        [NonSerialized]
        private static readonly ISave<User> serializer = ISave<User>.GetSerializer(SerializerOptions.BIN, FILE_NAME);

        [NonSerialized]
        private static List<User> users;

        private void CopyUser(User pUser)
        {
            IdUser = pUser.IdUser;
            Login = pUser.Login;
            Password = pUser.Password;
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string pLogin, string pPassword)
        {
            Login = pLogin;
            Password = pPassword;
        }

        public string Validate()
        {
            string result = Utils.ValidateString(Login, "login");
            return result += Utils.ValidateString(Password, "password");
        }

        public string Create()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdCheck = $"SELECT iduser FROM user WHERE login='{Login}';";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdCheck, out result);

                if (result.Length != 0)
                    return result;

                if (table.Rows.Count != 0)
                    return "User with that login already exists!\r\n";

                string cmdCreate = $"INSERT INTO user(login, password) VALUES('{Login}', '{Password}');";

                return ConnectionDB.GetInstance().Execute(cmdCreate);
            }
            else
            {
                try
                {
                    users = serializer.LoadList();

                    if (users.Find((user) => user.Login.Equals(Login)) != null)
                        return "User with that login already exists!\r\n";

                    users.Add(this);

                    serializer.SaveList(users);
                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }

                return result;
            }
        }

        public string Logon()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                //verify if user exists
                string cmd = $"SELECT * FROM user WHERE login='{Login}';";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmd, out result);

                if (result.Length != 0)
                    return result;

                if (table.Rows.Count != 0)
                {
                    //verify user password
                    if (table.Rows[0]["password"].ToString().Equals(Password))
                        result = "";
                    else
                        result = "Wrong password!\r\n";
                }
                else
                {
                    result = "No user with that login found!\r\n";
                }

                return result;
            }
            else
            {
                try
                {
                    users = serializer.LoadList();

                    User auxUser = users.Find((user) => user.Login.Equals(Login));

                    if (auxUser == null)
                        return "No user with that login found!\r\n";

                    if (!auxUser.Password.Equals(Password))
                        return "Wrong password!\r\n";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }
    }
}