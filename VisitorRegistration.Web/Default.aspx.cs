using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}