using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Clinica.Models
{
    [Serializable]
    public class Doctor : Person
    {
        public string Specialty { get; set; }
        public double Salary { get; set; }

        private static readonly string FILE_NAME = "Doctor";

        //[NonSerialized]
        //private static readonly ISave<Doctor> serializer = new SaveXml<Doctor>(FILE_NAME);

        [NonSerialized]
        private static readonly ISave<Doctor> serializer = new SaveBin<Doctor>(FILE_NAME);

        [NonSerialized]
        private static List<Doctor> doctors;

        private void CopyDoctor(Doctor pDoctor)
        {
            Name = pDoctor.Name;
            DateOfBirth = pDoctor.DateOfBirth;
            Gender = pDoctor.Gender;
            Telephone = pDoctor.Telephone;
            Zipcode = pDoctor.Zipcode;
            Specialty = pDoctor.Specialty;
            Salary = pDoctor.Salary;
        }

        //private static void SaveList()
        //{
        //    FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Create);

        //    XmlSerializer xml = new XmlSerializer(typeof(List<Doctor>));

        //    xml.Serialize(fs, doctors);

        //    fs.Flush();
        //    fs.Close();
        //}

        //private static void LoadList()
        //{
        //    if (File.Exists(Utils.DB_PATH + FILE_NAME))
        //    {
        //        FileStream fs = new FileStream(Utils.DB_PATH + FILE_NAME, FileMode.Open);

        //        XmlSerializer xml = new XmlSerializer(typeof(List<Doctor>));

        //        doctors = (List<Doctor>)xml.Deserialize(fs);

        //        fs.Close();
        //    }
        //    else
        //    {
        //        doctors = new List<Doctor>();
        //    }
        //}

        public Doctor() { }

        public Doctor(int pIdMedic) : base(pIdMedic)
        {
            Read();
        }

        public Doctor(Person pPerson)
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
            result += Utils.ValidateString(Specialty, "specialty");
            if (Salary < 0)
                result += "Invalid salary value.\r\n";
            return result;
        }

        public new string Create()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            //result = base.Create();

            //if (result.Length != 0)
            //    return result;

            //string cmdCreate = $"INSERT INTO doctor(iddoctor, specialty, salary) VALUES({IdPerson}, '{Specialty}', {Salary.ToString(CultureInfo.InvariantCulture)});";

            //return ConnectionDB.GetInstance().Execute(cmdCreate);
            doctors = serializer.LoadList();

            if (doctors.Find((doctor) => doctor.IdPerson == IdPerson) == null)
            {
                doctors.Add(this);
            }
            else
            {
                result = "Id already used!\r\n";
            }

            serializer.SaveList(doctors);

            return result;
        }

        public new string Read()
        {
            string result = Utils.ValidateId(IdPerson, "doctor");

            if (result.Length != 0)
                return result;

            //result = base.Read();

            //if (result.Length != 0)
            //    return result;

            //string cmdRead = $"SELECT * FROM doctor WHERE iddoctor={IdPerson};";

            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

            //if (result.Length != 0)
            //    return result;

            //if (table.Rows.Count != 0)
            //{
            //    foreach(DataRow row in table.Rows)
            //    {
            //        Specialty = row["specialty"].ToString();
            //        Salary = Convert.ToDouble(row["Salary"]);
            //    }
            //    result = "";
            //}
            //else
            //{
            //    result = "No results.\r\n";
            //}

            //return result;

            doctors = serializer.LoadList();

            Doctor auxDoctor = doctors.Find((doctor) => doctor.IdPerson == IdPerson);

            if (auxDoctor == null)
            {
                result = "No results\r\n";
            }
            else
            {
                CopyDoctor(auxDoctor);
            }

            return result;
        }

        public new string Update()
        {
            string result = Utils.ValidateId(IdPerson, "doctor");

            if (result.Length != 0)
                return result;

            //result = base.Update();

            //if (result.Length != 0)
            //    return result;

            //string cmdUpdate = $"UPDATE doctor SET specialty='{Specialty}', salary='{Salary.ToString(CultureInfo.InvariantCulture)}' WHERE iddoctor={IdPerson};";

            //return ConnectionDB.GetInstance().Execute(cmdUpdate);

            doctors = serializer.LoadList();

            int index = doctors.FindIndex((doctor) => doctor.IdPerson == IdPerson);

            if (index == -1)
            {
                result = "Doctor not found";
            }
            else
            {
                doctors[index] = this;
            }

            serializer.SaveList(doctors);

            return result;
        }

        public new string Delete()
        {
            string result = Utils.ValidateId(IdPerson, "doctor");

            //if (result.Length != 0)
            //    return result;

            //string cmdUpdate = $"DELETE FROM doctor WHERE iddoctor={IdPerson};";
            //result = ConnectionDB.GetInstance().Execute(cmdUpdate);

            //return base.Delete();

            doctors = serializer.LoadList();

            int index = doctors.FindIndex((doctor) => doctor.IdPerson == IdPerson);

            if (index == -1)
            {
                result = "Doctor not found";
            }
            else
            {
                doctors.RemoveAt(index);
            }

            serializer.SaveList(doctors);

            return result;
        }

        public new static List<Doctor> Read(string pCondition)
        {
            //List<Doctor> doctors = null;
            //string result;

            //string cmdRead = $"SELECT iddoctor FROM doctor INNER JOIN person ON iddoctor=idperson WHERE {pCondition};";



            //DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

            //if (result.Length != 0)
            //    return doctors;

            //doctors = new List<Doctor>();

            //if (table.Rows.Count != 0)
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        doctors.Add(new Doctor(Convert.ToInt32(row["iddoctor"])));
            //    }
            //}

            //return doctors;

            doctors = serializer.LoadList();

            return doctors;
        }
    }
}