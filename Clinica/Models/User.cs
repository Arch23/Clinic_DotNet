using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Clinica.Models
{
    [Serializable]
    public class User
    {
        private static readonly string FILE_NAME = "User";
        
        //[NonSerialized]
        //private static readonly ISave<User> serializer = new SaveXml<User>(FILE_NAME);

        [NonSerialized]
        private static readonly ISave<User> serializer = new SaveBin<User>(FILE_NAME);

        [NonSerialized]
        private static List<User> users;

        private void CopyUser(User pUser)
        {
            IdUser = pUser.IdUser;
            Login = pUser.Login;
            Password = pUser.Password;
        }

        //private static void SaveList()
        //{
        //    FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Create);

        //    XmlSerializer xml = new XmlSerializer(typeof(List<User>));

        //    xml.Serialize(fs, users);

        //    fs.Flush();
        //    fs.Close();
        //}

        //private static void LoadList()
        //{
        //    if (File.Exists(Utils.DB_PATH + FILE_NAME))
        //    {
        //        FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Open);

        //        XmlSerializer xml = new XmlSerializer(typeof(List<User>));

        //        users = (List<User>)xml.Deserialize(fs);

        //        fs.Close();
        //    }
        //    else
        //    {
        //        users = new List<User>();
        //    }
        //}

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

            //string cmdCheck = $"SELECT iduser FROM user WHERE login='{Login}';";

            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdCheck, out result);

            //if (result.Length != 0)
            //    return result;

            //if (table.Rows.Count!=0)
            //    return "User with that login already exists!\r\n";

            //string cmdCreate = $"INSERT INTO user(login, password) VALUES('{Login}', '{Password}');";

            //return ConnectionDB.GetInstance().Execute(cmdCreate);

            users = serializer.LoadList();

            if (users.Find((user) => user.Login.Equals(Login)) != null)
                return "User with that login already exists!\r\n";

            users.Add(this);

            serializer.SaveList(users);

            return result;
        }

        public string Logon()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            ////verify if user exists
            //string cmd = $"SELECT * FROM user WHERE login='{Login}';";

            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmd, out result);

            //if (result.Length != 0)
            //    return result;

            //if (table.Rows.Count != 0)
            //{
            //    //verify user password
            //    if (table.Rows[0]["password"].ToString().Equals(Password))
            //        result = "";
            //    else
            //        result = "Wrong password!\r\n";
            //}
            //else
            //{
            //    result = "No user with that login found!\r\n";
            //}

            //return result;

            users = serializer.LoadList();

            User auxUser = users.Find((user) => user.Login.Equals(Login));

            if (auxUser == null)
                return "No user with that login found!\r\n";

            if (!auxUser.Password.Equals(Password))
                return "Wrong password!\r\n";

            return result;
        }
    }
}