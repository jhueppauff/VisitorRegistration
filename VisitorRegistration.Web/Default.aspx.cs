using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisitorRegistration.Web
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }


        protected void SignIn_ServerClick(object sender, EventArgs e)
        {
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
            client.RegisterVisitor(inputFirstName.Value, inputLastName.Value, inputEmail.Value, inputHostEmail.Value, inputID.Value, signaturebinary.Value);

            //Reload Form

            Response.Redirect(Request.RawUrl);
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

        protected void EN_ServerClick(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage")
            {
                Value = "en-US"
            };
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void DE_ServerClick(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage")
            {
                Value = "de-DE"
            };
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected override void InitializeCulture()
        {
            string lang = string.Empty;
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];

            if (cookie != null && cookie.Value != null)
            {
                lang = cookie.Value;
                CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);

                System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = Cul;
            }
            else
            {
                if (string.IsNullOrEmpty(lang)) lang = "en-US";
                CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);

                System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = Cul;

                HttpCookie cookie_new = new HttpCookie("CurrentLanguage");
                cookie_new.Value = lang;
                Response.SetCookie(cookie_new);
            }

            base.InitializeCulture();
        }
    }
}