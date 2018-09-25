using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        private string HEADER = "Register new user";
        private string URL = "Register.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string result = ValidateFields();

            if (result.Length != 0)
                Utils.Error(Response, HEADER, result, URL);

            User user = new User(txtLogin.Text.Trim(), txtPassword.Text.Trim());

            result = user.Create();

            if (result.Length != 0)
                Utils.Error(Response, HEADER, result, URL);

            Response.Redirect("Login.aspx");
        }

        private string ValidateFields()
        {
            string result = Utils.ValidateTextBox(txtLogin, "login");
            result += Utils.ValidateTextBox(txtPassword, "password");
            result += Utils.ValidateTextBox(txtPasswordConf, "confirm password");

            if (!txtPassword.Text.Trim().Equals(txtPasswordConf.Text.Trim()))
                result += "Passwords dont match!\r\n";

            return result;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}