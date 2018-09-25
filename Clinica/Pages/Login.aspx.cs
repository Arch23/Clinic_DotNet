using Clinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private string HEADER = "Login";
        private string URL = "Login.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["login"];
           
            if(cookie != null)
            {
                txtLogin.Text = cookie["login"];
                ckbSave.Checked = Convert.ToBoolean(cookie["save"]);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string result = ValidateFields();

            if (result.Length != 0)
                Utils.Error(Response, HEADER, result, URL);

            User user = new User(txtLogin.Text.Trim(), txtPassword.Text.Trim());

            result = user.Logon();

            if (result.Length != 0)
                Utils.Error(Response, HEADER, result, URL);

            Session["authenticated"] = true;
            Session["username"] = txtLogin.Text.Trim();

            HttpCookie cookie = new HttpCookie("login");

            if (ckbSave.Checked)
            {
                cookie.Values.Add("login", txtLogin.Text.Trim());
                cookie.Values.Add("save", ckbSave.Checked.ToString());
                cookie.Expires = DateTime.Now.AddDays(1);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }

            Response.Cookies.Add(cookie);

            Response.Redirect("Home.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        private string ValidateFields()
        {
            string results = Utils.ValidateTextBox(txtLogin, "login");
            return results += Utils.ValidateTextBox(txtPassword, "password");
        }
    }
}