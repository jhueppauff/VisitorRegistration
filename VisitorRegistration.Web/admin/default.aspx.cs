using System;
using System.Text;

namespace VisitorRegistration.Web
{
    public partial class Admin : System.Web.UI.Page
    {
        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildTable();
            }
        }

        protected void BuildTable()
        {
            StringBuilder str = new StringBuilder();
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
            var Visitors = client.GetCurrentVisitors();

            foreach (var Visitor in Visitors)
            {
                str.AppendLine("<tr><th>" + Visitor.First_Name + "</th><th>" + Visitor.Last_Name + "</th><th>" + Visitor.Email + "</th><th>" + Visitor.HostEmail + "</th><th>" + Visitor.IDNumber + "</th><th><a runat='server' onclick='PrintPass("+Visitor.ID+")' class='btn btn-xs btn-success'><span class='glyphicon glyphicon-print'></span> Create Pass</a></th></tr>");
            }

            tbody.InnerHtml = str.ToString();
        }

        [System.Web.Services.WebMethod]
        public static Guid PrintVisitor(int ID)
        {
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();

            Guid FileID = client.GenerateVisitorPass(ID);
           

            return FileID;
        }

        
    }
}