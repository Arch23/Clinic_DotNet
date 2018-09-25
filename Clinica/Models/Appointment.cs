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
    public class Appointment
    {
        private static readonly string FILE_NAME = "Appointment";

        [NonSerialized]
        private static readonly ISave<Appointment> serializer = ISave<Appointment>.GetSerializer(SerializerOptions.BIN, FILE_NAME);

        [NonSerialized]
        private static List<Appointment> appointments;

        private void CopyAppointment(Appointment pAppointment)
        {
            Date = pAppointment.Date;
            Diagnosis = pAppointment.Diagnosis;
            Prescription = pAppointment.Prescription;
            ADoctor = pAppointment.ADoctor;
            APatient = pAppointment.APatient;
        }
        
        public int IdAppointment { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        private Doctor aDoctor;
        private Patient aPatient;
        
        public Appointment() { }

        public Appointment(int pId)
        {
            IdAppointment = pId;
            Read();
        }

        public Doctor ADoctor
        {
            get { return aDoctor; }
            set { aDoctor = value; }
        }

        public Patient APatient
        {
            get { return aPatient; }
            set { aPatient = value; }
        }

        public string Validate()
        {
            string result = Utils.ValidateId(IdAppointment, "appointment");
            result += Utils.ValidateString(Diagnosis, "diagnosis");
            result += Utils.ValidateString(Prescription, "prescription");
            if (aDoctor == null)
                result += "An appointment needs a medic.\r\n";
            if (aPatient == null)
                result += "An appointment needs a patient.\r\n";

            return result;
        }

        public string Create()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdCreate = $"INSERT INTO appointment(idappointment, diagnosis, prescription, date, iddoctor, idpatient) VALUES({IdAppointment}, '{Diagnosis}', '{Prescription}', '{Date.ToString("yyyy/MM/dd")}', {ADoctor.IdPerson}, {APatient.IdPerson});";

                return ConnectionDB.GetInstance().Execute(cmdCreate);
            }
            else
            {

                try
                {
                    appointments = serializer.LoadList();

                    if (appointments.Find((appointment) => appointment.IdAppointment == IdAppointment) == null)
                    {
                        appointments.Add(this);
                    }
                    else
                    {
                        result = "Id already used!\r\n";
                    }

                    serializer.SaveList(appointments);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }

        public string Read()
        {
            string result = Utils.ValidateId(IdAppointment, "appointment");

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdRead = $"SELECT * FROM appointment WHERE idappointment={IdAppointment};";

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return result;

                if (table.Rows.Count != 0)
                {
                    DataRow row = table.Rows[0];
                    Diagnosis = row["diagnosis"].ToString();
                    Prescription = row["prescription"].ToString();
                    ADoctor = new Doctor(Convert.ToInt32(row["iddoctor"]));
                    APatient = new Patient(Convert.ToInt32(row["idpatient"]));
                    Date = Convert.ToDateTime(row["date"]);
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
                    appointments = serializer.LoadList();

                    Appointment auxAppointment = appointments.Find((appointment) => appointment.IdAppointment == IdAppointment);

                    if (auxAppointment == null)
                    {
                        result = "No results\r\n";
                    }
                    else
                    {
                        CopyAppointment(auxAppointment);
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }

        public string Update()
        {
            string result = Validate();

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdUpdate = $"UPDATE appointment SET diagnosis='{Diagnosis}', prescription='{Prescription}', date='{Date.ToString("yyyy/MM/dd")}', iddoctor={ADoctor.IdPerson}, idpatient={APatient.IdPerson} WHERE idappointment={IdAppointment};";

                return ConnectionDB.GetInstance().Execute(cmdUpdate);
            }
            else
            {

                try
                {
                    appointments = serializer.LoadList();

                    int index = appointments.FindIndex((appointment) => appointment.IdAppointment == IdAppointment);

                    if (index == -1)
                    {
                        result = "Appointment not found";
                    }
                    else
                    {
                        appointments[index] = this;
                    }

                    serializer.SaveList(appointments);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }

        public string Delete()
        {
            string result = Utils.ValidateId(IdAppointment, "appointment");

            if (result.Length != 0)
                return result;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                string cmdDelete = $"DELETE FROM appointment WHERE idappointment={IdAppointment};";

                return ConnectionDB.GetInstance().Execute(cmdDelete);
            }
            else
            {
                try
                {
                    appointments = serializer.LoadList();

                    int index = appointments.FindIndex((appointment) => appointment.IdAppointment == IdAppointment);

                    if (index == -1)
                    {
                        result = "Appointment not found";
                    }
                    else
                    {
                        appointments.RemoveAt(index);
                    }

                    serializer.SaveList(appointments);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }

        public static List<Appointment> Read(string pCondition)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useDB"].ToString()))
            {
                List<Appointment> appointments = null;

                string cmdRead = $"SELECT idappointment FROM appointment WHERE {pCondition};";
                string result;

                DataTable table = ConnectionDB.GetInstance().ExecuteQuery(cmdRead, out result);

                if (result.Length != 0)
                    return appointments;

                appointments = new List<Appointment>();

                if (table.Rows.Count != 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        appointments.Add(new Appointment(Convert.ToInt32(row["idappointment"])));
                    }
                }

                return appointments;
            }
            else
            {
                try
                {
                    appointments = serializer.LoadList();
                }
                catch (Exception)
                {
                    appointments = null;
                }


                return appointments;
            }
        }
    }
}