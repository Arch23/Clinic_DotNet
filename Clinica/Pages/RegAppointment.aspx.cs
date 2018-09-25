using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class RegAppointment : System.Web.UI.Page
    {
        private string HEADER = "Appointment";
        private string URL = "RegAppointment.aspx";
        private Appointment appointment;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Doctor> doctors = Doctor.Read("true");
                dplDoctor.Items.Clear();
                if (doctors.Count != 0)
                {
                    foreach (Doctor doctor in doctors)
                    {
                        dplDoctor.Items.Add(new ListItem($"ID: {doctor.IdPerson} - {doctor.Name}", doctor.IdPerson.ToString()));
                    }
                }
                else
                {
                    dplDoctor.Items.Add(new ListItem("No doctor registered.", "-1"));
                }

                List<Patient> patients = Patient.Read("true");
                dplPatient.Items.Clear();
                if (patients.Count != 0)
                {
                    foreach (Patient patient in patients)
                    {
                        dplPatient.Items.Add(new ListItem($"ID: {patient.IdPerson} - {patient.Name}", patient.IdPerson.ToString()));
                    }
                }
                else
                {
                    dplPatient.Items.Add(new ListItem("No patients registered.", "-1"));
                }
            }
        }

        protected void txtRegister_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            CreateAppointment();

            results = appointment.Create();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Appointment registered!");
            ClearFields();
        }

        protected void txtUpdate_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            CreateAppointment();

            results = appointment.Update();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Appointment updated!");
        }

        protected void txtDelete_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Appointment appointment = new Appointment
            {
                IdAppointment = Convert.ToInt32(txtId.Text)
            };

            results = appointment.Delete();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Appointment deleted!");
            ClearFields();
        }

        protected void txtLoad_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Appointment appointment = new Appointment
            {
                IdAppointment = Convert.ToInt32(txtId.Text)
            };

            results = appointment.Read();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            txtId.Text = appointment.IdAppointment.ToString();
            txtDate.Text = appointment.Date.ToString("yyyy-MM-dd");
            txtDiagnosis.Text = appointment.Diagnosis;
            txtPrescription.Text = appointment.Prescription;
            dplPatient.SelectedValue = appointment.APatient.IdPerson.ToString();
            dplDoctor.SelectedValue = appointment.ADoctor.IdPerson.ToString();
        }

        private void CreateAppointment()
        {
            appointment = new Appointment
            {
                IdAppointment = Convert.ToInt32(txtId.Text),
                Date = Convert.ToDateTime(txtDate.Text),
                Diagnosis = txtDiagnosis.Text.Trim(),
                Prescription = txtPrescription.Text.Trim(),
                APatient = new Patient(Convert.ToInt32(dplPatient.SelectedValue)),
                ADoctor = new Doctor(Convert.ToInt32(dplDoctor.SelectedValue))
            };
        }

        private string ValidateId()
        {
            return txtId.Text.Trim().Length == 0 ? "Id cant be empty.\r\n" : "";
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtDate.Text = "";
            txtDiagnosis.Text = "";
            txtPrescription.Text = "";
        }

        private string ValidateFields()
        {
            string results = Utils.ValidateTextBox(txtId, "appointment's id");
            results += Utils.ValidateTextBox(txtDate, "date");
            results += Utils.ValidateTextBox(txtDiagnosis, "diagnosis");
            results += Utils.ValidateTextBox(txtPrescription, "prescription");

            if (dplDoctor.SelectedValue == "-1")
                results += "No doctor registered!";

            if (dplPatient.SelectedValue == "-1")
                results += "No patient registered!";

            return results;
        }
    }
}