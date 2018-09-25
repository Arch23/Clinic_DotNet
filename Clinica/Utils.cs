using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Clinica
{
    public static class Utils
    {
        private static readonly string HTML_NL = "<br/>";
        private static readonly string CODE_NL = "\r\n";
        private static readonly string TOKEN_NL = "@NL";

        public static readonly string DB_PATH = "C://TSC_CLINIC//DB//";

        public static string ValidateTextBox(TextBox pTextBox, string pFieldName)
        {
            string result = "";
            if (pTextBox.Text.Trim().Length == 0)
            {
                result = $"{pFieldName} cant be empty.\r\n";
            }
            return result;
        }

        public static void Alert(HttpResponse pResponse, string pMessage)
        {
            pResponse.Write($"<script>alert('{DecodeStringHTML(EncodeString(pMessage))}')</script>");
        }

        public static void Error(HttpResponse pResponse, string pHeader, string pBody, string pBack)
        {
            string url = $"Error.aspx?header={EncodeString(pHeader)}&body={EncodeString(pBody)}&back={pBack}";
            Log.Creator.WriteLog(DecodeStringCode(EncodeString(pBody)));
            pResponse.Redirect(url);
        }

        public static string EncodeString(string pText)
        {
            pText = pText.Replace(HTML_NL, TOKEN_NL);
            return pText.Replace(CODE_NL, TOKEN_NL);
        }

        public static string DecodeStringHTML(string pText)
        {
            return pText.Replace(TOKEN_NL, HTML_NL);
        }

        public static string DecodeStringCode(string pText)
        {
            return pText.Replace(TOKEN_NL, CODE_NL);
        }

        public static string ValidateString(string pText, string pName)
        {
            return pText.Trim().Length == 0 ? $"{pName} cant be empty" : "";
        }

        public static string ValidateId(int pId, string pName)
        {
            return pId < 0 ? $"id {pName} not valid.\r\n":"";
        }

        public static string ValidateDouble(string text)
        {
            Regex regex = new Regex("^\\d+([,\\.]\\d{1,2})?$");
            return regex.IsMatch(text)?"":"Invalid decimal number.";
        }
    }
}