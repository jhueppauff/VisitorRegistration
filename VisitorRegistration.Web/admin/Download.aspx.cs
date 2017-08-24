using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisitorRegistration.Web.admin
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["ID"]!="")
            {
                VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
                byte[] file = client.DownloadVisitorPass(Guid.Parse( Request.QueryString["ID"]));


                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Response.AddHeader("Content-Disposition", "attachment; filename=visitorpass.docx");
                Response.OutputStream.Write(file, 0, file.Length);
                Response.Flush();


            }
        }
    }
}