//-----------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="None">
// Copyright 2017 Jhueppauff
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------
namespace VisitorRegistration.Web
{
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Default Page
    /// </summary>
    public partial class Default : Page
    {
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"><see cref="Object"/> Sender</param>
        /// <param name="e"><see cref="EventArgs"/> e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Server OnClick Event "SignIn_ServerClick
        /// </summary>
        /// <param name="sender"><see cref="Object"/> Sender</param>
        /// <param name="e"><see cref="EventArgs"/> e</param>
        protected void SignIn_ServerClick(object sender, EventArgs e)
        {
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
            client.RegisterVisitor(this.inputFirstName.Value, this.inputLastName.Value, this.inputEmail.Value, this.inputHostEmail.Value, this.inputID.Value, this.signaturebinary.Value);

            // Reload Form
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Clear all Text Boxes
        /// </summary>
        /// <param name="p1">Web Control</param>
        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
#pragma warning disable IDE0019 // Use pattern matching
                    TextBox t = ctrl as TextBox;
#pragma warning restore IDE0019 // Use pattern matching

                    t.Text = string.Empty;
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        this.ClearTextBoxes(ctrl);
                    }
                }
            }
        }

        /// <summary>
        /// Change Language to English
        /// </summary>
        /// <param name="sender"><see cref="Object"/> Sender</param>
        /// <param name="e"><see cref="EventArgs"/> e</param>
        protected void EN_ServerClick(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage")
            {
                Value = "en-US"
            };
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Change Language to German
        /// </summary>
        /// <param name="sender"><see cref="Object"/> Sender</param>
        /// <param name="e"><see cref="EventArgs"/> e</param>
        protected void DE_ServerClick(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage")
            {
                Value = "de-DE"
            };
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Load Culture
        /// </summary>
        protected override void InitializeCulture()
        {
            string lang = string.Empty;
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];

            if (cookie != null && cookie.Value != null)
            {
                lang = cookie.Value;
                CultureInfo cul = CultureInfo.CreateSpecificCulture(lang);

                System.Threading.Thread.CurrentThread.CurrentUICulture = cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = cul;
            }
            else
            {
                if (string.IsNullOrEmpty(lang))
                {
                    lang = "en-US";
                }

                CultureInfo cul = CultureInfo.CreateSpecificCulture(lang);

                System.Threading.Thread.CurrentThread.CurrentUICulture = cul;
                System.Threading.Thread.CurrentThread.CurrentCulture = cul;

                HttpCookie cookie_new = new HttpCookie("CurrentLanguage")
                {
                    Value = lang
                };
                Response.SetCookie(cookie_new);
            }

            base.InitializeCulture();
        }
    }
}