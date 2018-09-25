using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class RegPatient : System.Web.UI.Page
    {
        private string HEADER = "Patient";
        private string URL = "RegPatient.aspx";
        private Patient patient;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtRegister_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            CreatePatient();

            results = patient.Create();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Patient registered!");
            ClearFields();
        }

        protected void txtUpdate_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            CreatePatient();

            results = patient.Update();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Patient updated!");
        }

        protected void txtDelete_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Patient patient = new Patient
            {
                IdPerson = Convert.ToInt32(txtId.Text)
            };

            results = patient.Delete();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Patient deleted!");
            ClearFields();
        }

        protected void txtLoad_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Patient patient = new Patient
            {
                IdPerson = Convert.ToInt32(txtId.Text)
            };

            results = patient.Read();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            txtName.Text = patient.Name;
            txtDateOfBirth.Text = patient.DateOfBirth.ToString("yyyy-MM-dd");
            rbtMale.Checked = patient.Gender == "M";
            rbtFemale.Checked = patient.Gender == "F";
            txtTelephone.Text = patient.Telephone;
            txtZipcode.Text = patient.Zipcode;
            txtMother.Text = patient.Mother;
            txtFather.Text = patient.Father;
            txtHypertensive.Text = patient.Hypertensive;
        }

        private void CreatePatient()
        {
            patient = new Patient
            {
                IdPerson = Convert.ToInt32(txtId.Text),
                Name = txtName.Text.Trim(),
                DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                Gender = rbtMale.Checked ? "M" : "F",
                Telephone = txtTelephone.Text.Trim(),
                Zipcode = txtZipcode.Text.Trim(),
                Mother = txtMother.Text.Trim(),
                Father = txtFather.Text.Trim(),
                Hypertensive = txtHypertensive.Text.Trim()
            };
        }

        private string ValidateId()
        {
            return txtId.Text.Trim().Length == 0 ? "Id cant be empty.\r\n" : "";
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtDateOfBirth.Text = "";
            rbtMale.Checked = true;
            rbtFemale.Checked = false;
            txtTelephone.Text = "";
            txtZipcode.Text = "";

            txtFather.Text = "";
            txtMother.Text = "";
            txtHypertensive.Text = "";
        }

        private string ValidateFields()
        {
            string results = Utils.ValidateTextBox(txtId, "doctor id");
            results += Utils.ValidateTextBox(txtName, "name");
            results += Utils.ValidateTextBox(txtDateOfBirth, "date of birth");
            results += Utils.ValidateTextBox(txtTelephone, "telephone");
            results += Utils.ValidateTextBox(txtZipcode, "zipcode");

            results += Utils.ValidateTextBox(txtFather, "father's name");
            results += Utils.ValidateTextBox(txtMother, "mother's name");
            results += Utils.ValidateTextBox(txtHypertensive, "hypertensive");

            return results;
        }
    }
}