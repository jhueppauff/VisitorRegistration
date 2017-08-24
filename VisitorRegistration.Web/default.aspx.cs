using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisitorRegistration.Web
{
    public partial class Default : Page
    {
        string lang = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeCulture();
        }

        protected void SignIn_ServerClick(object sender, EventArgs e)
        {
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
            client.RegisterVisitor(inputFirstName.Value, inputLastName.Value, inputEmail.Value, inputHostEmail.Value, inputID.Value, signaturebinary.Value);

            //Reload Form

            Response.Redirect(Request.RawUrl);
        }

        protected override void InitializeCulture()
        {
            var currentLanguage = HttpContext.Current.Request.Cookies["lang"];
            string defaultLanguage = "en-US";
            if (currentLanguage == null)
            {
                HttpCookie hc = new HttpCookie("lang")
                {
                    Expires = DateTime.Now.AddDays(30),
                    Value = defaultLanguage
                };
                HttpContext.Current.Response.Cookies.Add(hc);
                Server.Transfer(Request.Path);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentLanguage.Value);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            }

        }
        protected void ChangeLanguageGerman_Click(object sender, EventArgs e)
        {
            lang = "de-DE";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            HttpCookie hc = new HttpCookie("lang")
            {
                Expires = DateTime.Now.AddDays(30),
                Value = lang
            };
            HttpContext.Current.Response.Cookies.Add(hc);
            Server.Transfer(Request.Path);
        }
        protected void ChangeLanguageEnglish_Click(object sender, EventArgs e)
        {
            lang = "en-US";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            HttpCookie hc = new HttpCookie("lang")
            {
                Expires = DateTime.Now.AddDays(30),
                Value = lang
            };
            HttpContext.Current.Response.Cookies.Add(hc);
            Server.Transfer(Request.Path);
        }
        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
#pragma warning disable IDE0019 // Use pattern matching
                    TextBox t = ctrl as TextBox;
#pragma warning restore IDE0019 // Use pattern matching


                    t.Text = String.Empty;

                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearTextBoxes(ctrl);
                    }
                }
            }
        }
    }

}