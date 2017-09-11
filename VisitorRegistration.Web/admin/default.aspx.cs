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
namespace VisitorRegistration.Web.Admin
{
    using System;
    using System.Text;

    /// <summary>
    /// Default Page
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Gets the File ID (Ajax BackEnd WebMethod)
        /// </summary>
        /// <param name="id">Visitor ID</param>
        /// <returns>File ID</returns>
        [System.Web.Services.WebMethod]
        public static Guid PrintVisitor(int id)
        {
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();

            Guid fileID = client.GenerateVisitorPass(id);
            return fileID;
        }

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"><see cref="Object"/> Sender</param>
        /// <param name="e"><see cref="EventArgs"/> e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BuildTable();
            }
        }

        /// <summary>
        /// Build Visitor Table
        /// </summary>
        protected void BuildTable()
        {
            StringBuilder str = new StringBuilder();
            VisitorRegistrationService.EndpointClient client = new VisitorRegistrationService.EndpointClient();
            var visitors = client.GetCurrentVisitors();

            foreach (var visitor in visitors)
            {
                str.AppendLine("<tr><th>" + visitor.First_Name + "</th><th>" + visitor.Last_Name + "</th><th>" + visitor.Email + "</th><th>" + visitor.HostEmail + "</th><th>" + visitor.IDNumber + "</th><th><a runat='server' onclick='PrintPass(" + visitor.ID + ")' class='btn btn-xs btn-success'><span class='glyphicon glyphicon-print'></span> Create Pass</a></th></tr>");
            }

            this.tbody.InnerHtml = str.ToString();
        } 
    }
}