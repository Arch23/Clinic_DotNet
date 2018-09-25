using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Clinica.Models
{
    [Serializable]
    public class Person
    {
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Zipcode { get; set; }

        private static readonly string FILE_NAME = "People";

        [NonSerialized]
        private static readonly ISave<Person> serializer = ISave<Person>.GetSerializer(SerializerOptions.BIN, FILE_NAME);

        [NonSerialized]
        private static List<Person> people;

        public Person() { }

        public Person(int pIdPerson)
        {
            IdPerson = pIdPerson;
            Read();
        }

        private void CopyPerson(Person pPerson)
        {
            Name = pPerson.Name;
            DateOfBirth = pPerson.DateOfBirth;
            Gender = pPerson.Gender;
            Telephone = pPerson.Telephone;
            Zipcode = pPerson.Zipcode;
        }

        protected virtual string Validate()
        {
            string result = Utils.ValidateId(IdPerson, "person");
            result += Utils.ValidateString(Name, "name");
            result += Utils.ValidateString(Gender, "gender");
            result += Utils.ValidateString(Telephone, "telephone");
            return result += Utils.ValidateString(Zipcode, "zipcode");
        }

        //private static void SaveList()
        //{
        //    FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Create);

        //    XmlSerializer xml = new XmlSerializer(typeof(List<Person>));

        //    xml.Serialize(fs,people);

        //    fs.Flush();
        //    fs.Close();
        //}

        //private static void LoadList()
        //{
        //    if (File.Exists(Utils.DB_PATH+FILE_NAME))
        //    {
        //        FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Open);

        //        XmlSerializer xml = new XmlSerializer(typeof(List<Person>));

        //        people = (List<Person>)xml.Deserialize(fs);

        //        fs.Close();
        //    }
        //    else
        //    {
        //        people = new List<Person>();
        //    }
        //}

        public virtual string Create()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            //string cmdCreate = $"INSERT INTO person(idperson, name, dateofbirth, gender, telephone, zipcode) VALUES({IdPerson}, '{Name}', '{DateOfBirth.ToString("yyyy/MM/dd")}', '{Gender}', '{Telephone}', '{Zipcode}');";

            //return ConnectionDB.GetInstance().Execute(cmdCreate);

            try
            {
                people = serializer.LoadList();

                if (people.Find((person) => person.IdPerson == IdPerson) == null)
                {
                    people.Add(this);
                }
                else
                {
                    result = "Id already used!\r\n";
                }

                serializer.SaveList(people);
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;
        }

        public virtual string Read()
        {
            string result = Utils.ValidateId(IdPerson, "person");

            if (result.Length != 0)
                return result;

            //string cmdRead = $"SELECT * FROM person WHERE idperson={IdPerson}";

            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

            //if (result.Length != 0)
            //    return result;

            //if (table.Rows.Count != 0)
            //{
            //    foreach(DataRow row in table.Rows)
            //    {
            //        Name = row["name"].ToString();
            //        DateOfBirth = Convert.ToDateTime(row["dateofbirth"]);
            //        Gender = row["gender"].ToString();
            //        Telephone = row["telephone"].ToString();
            //        Zipcode = row["zipcode"].ToString();
            //    }
            //    result = "";
            //}
            //else
            //{
            //    result = "no results.\r\n";
            //}

            try
            {
                people = serializer.LoadList();

                Person auxPerson = people.Find((person) => person.IdPerson == IdPerson);

                if (auxPerson == null)
                {
                    result = "No results\r\n";
                }
                else
                {
                    CopyPerson(auxPerson);
                }
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }
            
            return result;
        }

        public virtual string Update()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            //string cmdUpdate = $"UPDATE person SET name='{Name}', dateofbirth='{DateOfBirth.ToString("yyyy/MM/dd")}', gender='{Gender}', telephone='{Telephone}', zipcode='{Zipcode}' WHERE idperson={IdPerson};";

            //return ConnectionDB.GetInstance().Execute(cmdUpdate);

            try
            {
                people = serializer.LoadList();

                int index = people.FindIndex((person) => person.IdPerson == IdPerson);

                if (index == -1)
                {
                    result = "Person not found";
                }
                else
                {
                    people[index] = this;
                }

                serializer.SaveList(people);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public virtual string Delete()
        {
            string result = Utils.ValidateId(IdPerson, "person");

            if (result.Length != 0)
                return result;

            //string cmdDelete = $"DELETE FROM person WHERE idperson={IdPerson};";

            //return ConnectionDB.GetInstance().Execute(cmdDelete);

            try
            {
                people = serializer.LoadList();

                int index = people.FindIndex((person) => person.IdPerson == IdPerson);

                if (index == -1)
                {
                    result = "Person not found";
                }
                else
                {
                    people.RemoveAt(index);
                }

                serializer.SaveList(people);

            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;
        }

        public static List<Person> Read(string pCondition)
        {
            //string cmdRead = $"SELECT idperson FROM person WHERE {pCondition}";
            //string result;
            //List<Person> people = null;

            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

            //if (result.Length != 0)
            //    return people;

            //people = new List<Person>();

            //if (table.Rows.Count != 0)
            //{
            //    foreach(DataRow row in table.Rows)
            //    {
            //        people.Add(new Person(Convert.ToInt32(row["idperson"])));
            //    }
            //}

            //return people;

            try
            {
                people = serializer.LoadList();
            }
            catch (Exception)
            {

                people = null;
            }
            
            return people;
        }
    }
}