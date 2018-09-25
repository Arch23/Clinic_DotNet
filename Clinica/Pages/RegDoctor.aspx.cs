using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class RegDoctor : System.Web.UI.Page
    {
        private string HEADER = "Doctor";
        private string URL = "RegDoctor.aspx";
        private Doctor doctor;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtRegister_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            CreateDoctor();

            results = doctor.Create();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Doctor registered!");
            ClearFields();
        }

        protected void txtUpdate_Click(object sender, EventArgs e)
        {
            string results = ValidateFields();
            
            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);
            
            CreateDoctor();

            results = doctor.Update();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);
            
            Utils.Alert(Response, "Doctor updated!");
        }

        protected void txtDelete_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Doctor doctor = new Doctor {
                IdPerson = Convert.ToInt32(txtId.Text)
            };

            results = doctor.Delete();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Utils.Alert(Response, "Doctor deleted!");
            ClearFields();
        }

        protected void txtLoad_Click(object sender, EventArgs e)
        {
            string results = ValidateId();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            Doctor doctor = new Doctor
            {
                IdPerson = Convert.ToInt32(txtId.Text)
            };

            results = doctor.Read();

            if (results.Length != 0)
                Utils.Error(Response, HEADER, results, URL);

            txtName.Text = doctor.Name;
            txtDateOfBirth.Text = doctor.DateOfBirth.ToString("yyyy-MM-dd");
            rbtMale.Checked = doctor.Gender == "M";
            rbtFemale.Checked = doctor.Gender == "F";
            txtTelephone.Text = doctor.Telephone;
            txtZipcode.Text = doctor.Zipcode;
            txtSpecialty.Text = doctor.Specialty;
            txtSalary.Text = doctor.Salary.ToString(CultureInfo.InvariantCulture);
        }

        private void CreateDoctor()
        {
            doctor = new Doctor
            {
                IdPerson = Convert.ToInt32(txtId.Text),
                Name = txtName.Text.Trim(),
                DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                Gender = rbtMale.Checked ? "M" : "F",
                Telephone = txtTelephone.Text.Trim(),
                Zipcode = txtZipcode.Text.Trim(),
                Specialty = txtSpecialty.Text.Trim(),
                Salary = Convert.ToDouble(txtSalary.Text, CultureInfo.InvariantCulture)
            };
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

            txtSpecialty.Text = "";
            txtSalary.Text = "";
        }

        private string ValidateId()
        {
            return txtId.Text.Trim().Length == 0 ? "Id cant be empty.\r\n" : "";
        }

        private string ValidateFields()
        {
            string results = Utils.ValidateTextBox(txtId, "doctor id");
            results += Utils.ValidateTextBox(txtName, "name");
            results += Utils.ValidateTextBox(txtDateOfBirth, "date of birth");
            results += Utils.ValidateTextBox(txtTelephone, "telephone");
            results += Utils.ValidateTextBox(txtZipcode, "zipcode");


            results += Utils.ValidateTextBox(txtSpecialty, "specialty");
            results += Utils.ValidateTextBox(txtSalary, "salary");

            results += Utils.ValidateDouble(txtSalary.Text);
            
            return results;
        }
    }
}