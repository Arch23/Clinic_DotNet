using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinica.Pages
{
    public partial class Error : System.Web.UI.Page
    {
        private string BACK_URL;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeaderContent.Text = Utils.DecodeStringHTML(Request.QueryString["header"].ToString());
            lblBodyContent.Text = Utils.DecodeStringHTML(Request.QueryString["body"].ToString());
            BACK_URL = Request.QueryString["back"].ToString();
        }

        protected void txtBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BACK_URL);
        }
    }
}