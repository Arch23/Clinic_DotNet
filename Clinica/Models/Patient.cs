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
    public class Patient : Person
    {
        private static readonly string FILE_NAME = "Patient";

        [NonSerialized]
        private static readonly ISave<Patient> serializer = ISave<Patient>.GetSerializer(SerializerOptions.BIN, FILE_NAME);

        [NonSerialized]
        private static List<Patient> patients;

        private void CopyPatient(Patient pPatient)
        {
            Name = pPatient.Name;
            DateOfBirth = pPatient.DateOfBirth;
            Gender = pPatient.Gender;
            Telephone = pPatient.Telephone;
            Zipcode = pPatient.Zipcode;
            Mother = pPatient.Mother;
            Father = pPatient.Father;
            Hypertensive = pPatient.Hypertensive;
        }
        
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Hypertensive { get; set; }

        public Patient() { }

        public Patient(int pIdPatient) : base(pIdPatient)
        {
            Read();
        }

        public Patient(Person pPerson)
        {
            IdPerson = pPerson.IdPerson;
            Name = pPerson.Name;
            DateOfBirth = pPerson.DateOfBirth;
            Gender = pPerson.Gender;
            Telephone = pPerson.Telephone;
            Zipcode = pPerson.Zipcode;
            Read();
        }

        private new string Validate()
        {
            string result = base.Validate();
            result += Utils.ValidateString(Father, "father's name");
            return result += Utils.ValidateString(Mother, "mother's name");
        }

        public new string Create()
        {
            string result = Utils.ValidateId(IdPerson, "patient");

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                result = base.Create();

                if (result.Length != 0)
                    return result;

                string cmdCreate = $"INSERT INTO patient(idpatient, mother, father, hypertensive) VALUES({IdPerson}, '{Mother}', '{Father}', '{Hypertensive}');";

                return ConnectionDB.GetInstance().Execute(cmdCreate);
            }
            else
            {
                try
                {
                    patients = serializer.LoadList();

                    if (patients.Find((patient) => patient.IdPerson == IdPerson) == null)
                    {
                        patients.Add(this);
                    }
                    else
                    {
                        result = "Id already used!\r\n";
                    }

                    serializer.SaveList(patients);
                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }

                return result;
            }
        }

        public new string Read()
        {
            string result = Utils.ValidateId(IdPerson, "patient");

            if (result.Length != 0)
                return result;


            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                result = base.Read();

                if (result.Length != 0)
                    return result;

                string cmdRead = $"SELECT * FROM patient WHERE idpatient={IdPerson};";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return result;

                if (table.Rows.Count != 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Mother = row["mother"].ToString();
                        Father = row["father"].ToString();
                        Hypertensive = row["hypertensive"].ToString();
                    }
                    result = "";
                }
                else
                {
                    result = "No results.\r\n";
                }

                return result;
            }
            else
            {
                try
                {
                    patients = serializer.LoadList();

                    Patient auxPatient = patients.Find((patient) => patient.IdPerson == IdPerson);

                    if (auxPatient == null)
                    {
                        result = "No results\r\n";
                    }
                    else
                    {
                        CopyPatient(auxPatient);
                    }

                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }


                return result;
            }
        }

        public new string Update()
        {
            string result = Utils.ValidateId(IdPerson, "patient");

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                result = base.Update();

                if (result.Length != 0)
                    return result;

                string cmdUpdate = $"UPDATE patient SET mother='{Mother}', father='{Father}', hypertensive='{Hypertensive}' WHERE idpatient={IdPerson};";

                return ConnectionDB.GetInstance().Execute(cmdUpdate);
            }
            else
            {
                try
                {
                    patients = serializer.LoadList();

                    int index = patients.FindIndex((patient) => patient.IdPerson == IdPerson);

                    if (index == -1)
                    {
                        result = "Patient not found";
                    }
                    else
                    {
                        patients[index] = this;
                    }

                    serializer.SaveList(patients);
                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }

                return result;
            }
        }

        public new string Delete()
        {
            string result = Utils.ValidateId(IdPerson, "patient");

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdDelete = $"DELETE FROM patient WHERE idpatient={IdPerson};";

                result = ConnectionDB.GetInstance().Execute(cmdDelete);

                if (result.Length != 0)
                    return result;

                return base.Delete();
            }
            else
            {
                try
                {
                    patients = serializer.LoadList();

                    int index = patients.FindIndex((patient) => patient.IdPerson == IdPerson);

                    if (index == -1)
                    {
                        result = "Patient not found";
                    }
                    else
                    {
                        patients.RemoveAt(index);
                    }

                    serializer.SaveList(patients);
                }
                catch (Exception ex)
                {

                    result = ex.Message;
                }

                return result;
            }
        }

        public new static List<Patient> Read(string pCondition)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                List<Patient> patients = null;
                string result;

                string cmdRead = $"SELECT idpatient FROM patient INNER JOIN person ON idpatient=idperson WHERE {pCondition};";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return patients;

                patients = new List<Patient>();

                if (table.Rows.Count != 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        patients.Add(new Patient(Convert.ToInt32(row["idpatient"])));
                    }
                }

                return patients;
            }
            else
            {
                try
                {
                    patients = serializer.LoadList();
                }
                catch (Exception)
                {

                    patients = null;
                }

                return patients;
            }
        }
    }
}