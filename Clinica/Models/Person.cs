using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static readonly string FILE_NAME = "People";

        [NonSerialized]
        private static readonly ISave<Person> serializer = ISave<Person>.GetSerializer(SerializerOptions.BIN, FILE_NAME);

        [NonSerialized]
        private static List<Person> people;


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

        public int IdPerson { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Zipcode { get; set; }
        
        public Person() { }

        public Person(int pIdPerson)
        {
            IdPerson = pIdPerson;
            Read();
        }

        public virtual string Create()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdCreate = $"INSERT INTO person(idperson, name, dateofbirth, gender, telephone, zipcode) VALUES({IdPerson}, '{Name}', '{DateOfBirth.ToString("yyyy/MM/dd")}', '{Gender}', '{Telephone}', '{Zipcode}');";

                return ConnectionDB.GetInstance().Execute(cmdCreate);
            }
            else
            {
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
        }

        public virtual string Read()
        {
            string result = Utils.ValidateId(IdPerson, "person");

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdRead = $"SELECT * FROM person WHERE idperson={IdPerson}";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return result;

                if (table.Rows.Count != 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Name = row["name"].ToString();
                        DateOfBirth = Convert.ToDateTime(row["dateofbirth"]);
                        Gender = row["gender"].ToString();
                        Telephone = row["telephone"].ToString();
                        Zipcode = row["zipcode"].ToString();
                    }
                    result = "";
                }
                else
                {
                    result = "no results.\r\n";
                }
            }
            else
            {
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
            }
            return result;
        }

        public virtual string Update()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdUpdate = $"UPDATE person SET name='{Name}', dateofbirth='{DateOfBirth.ToString("yyyy/MM/dd")}', gender='{Gender}', telephone='{Telephone}', zipcode='{Zipcode}' WHERE idperson={IdPerson};";

                return ConnectionDB.GetInstance().Execute(cmdUpdate);
            }
            else
            {
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
        }

        public virtual string Delete()
        {
            string result = Utils.ValidateId(IdPerson, "person");

            if (result.Length != 0)
                return result;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdDelete = $"DELETE FROM person WHERE idperson={IdPerson};";

                return ConnectionDB.GetInstance().Execute(cmdDelete);
            }
            else
            {
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
        }

        public static List<Person> Read(string pCondition)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdRead = $"SELECT idperson FROM person WHERE {pCondition}";
                string result;
                List<Person> people = null;

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return people;

                people = new List<Person>();

                if (table.Rows.Count != 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        people.Add(new Person(Convert.ToInt32(row["idperson"])));
                    }
                }

                return people;
            }
            else
            {
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
}