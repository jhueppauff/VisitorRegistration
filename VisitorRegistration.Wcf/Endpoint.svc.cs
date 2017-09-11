//-----------------------------------------------------------------------
// <copyright file="Endpoint.svc.cs" company="None">
// Copyright 2017 Jhueppauff
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------
namespace VisitorRegistration.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Endpoint Class
    /// </summary>
    public class Endpoint : IEndpoint
    {
        /// <summary>
        /// Register a new Visitor
        /// </summary>
        /// <param name="firstName">First Name of the Visitor</param>
        /// <param name="lastName">Last Name of the Visitor</param>
        /// <param name="email">eMail of the Visitor</param>
        /// <param name="hostEmail">eMail of the responsible Host</param>
        /// <param name="idNumber">ID Number</param>
        /// <param name="signature">Signature Bash64 String</param>
        public void RegisterVisitor(string firstName, string lastName, string email, string hostEmail, string idNumber, string signature)
        {
            var visitor = new Visitor()
            {
                DateOfVisit = DateTime.Now,
                Email = email,
                Last_Name = lastName,
                First_Name = firstName,
                IDNumber = idNumber,
                Printed = false,
                HostEmail = hostEmail,
                Signature = signature
            };

            using (var db = new VisitorRegistrationEntities())
            {
                db.Visitors.Add(visitor);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get Visitor by ID
        /// </summary>
        /// <param name="id">ID of the Visitor</param>
        /// <returns><see cref="Visitor"/></returns>
        public Visitor GetVisitor(int id)
        {
            using (var db = new VisitorRegistrationEntities())
            {
                var visitor = from v in db.Visitors where v.ID == id select v;

                Visitor visitorEntry = visitor.FirstOrDefault();
                return visitorEntry;
            }
        }

        /// <summary>
        /// Get a List of the type<see cref="Visitor"/> from the daily visitors
        /// </summary>
        /// <returns>Returns a list of<see cref="Visitor"/></returns>
        public List<Visitor> GetCurrentVisitors()
        {
            using (var db = new VisitorRegistrationEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var visitor = from v in db.Visitors where v.DateOfVisit == DateTime.Today select v;

                List<Visitor> visitorEntrys = new List<Visitor>();
                foreach (Visitor visitorEntry in visitor)
                {
                    visitorEntry.Signature = null;
                    visitorEntrys.Add(visitorEntry);
                }

                return visitorEntrys;
            }
        }

        /// <summary>
        /// Generates the Visitor Pass
        /// </summary>
        /// <param name="id">Visitor ID</param>
        /// <returns>Returns the File ID as <see cref="Guid"/></returns>
        public Guid GenerateVisitorPass(int id)
        {
            Visitor visitor = this.GetVisitor(id);
            Guid fileID = Guid.NewGuid();

            var application = new Microsoft.Office.Interop.Word.Application();
            var document = application.Documents.Add(Template: Properties.Settings.Default.TemplateFile);

            application.Visible = false;

            foreach (Microsoft.Office.Interop.Word.MailMergeField field in document.MailMerge.Fields)
            {
                if (field.Code.Text.Contains("FirstName"))
                {
                    field.Select();
                    application.Selection.TypeText(visitor.First_Name);
                }
                else if (field.Code.Text.Contains("LastName"))
                {
                    field.Select();
                    application.Selection.TypeText(visitor.Last_Name);
                }
                else if (field.Code.Text.Contains("Email"))
                {
                    field.Select();
                    application.Selection.TypeText(visitor.Email);
                }
                else if (field.Code.Text.Contains("DateOfVisit"))
                {
                    field.Select();
                    application.Selection.TypeText(visitor.DateOfVisit.ToString());
                }
                else if (field.Code.Text.Contains("HostEmail"))
                {
                    field.Select();
                    application.Selection.TypeText(visitor.HostEmail);
                }
            }

            document.Fields.Update();

            document.SaveAs2(FileName: Path.Combine(Properties.Settings.Default.FileDropLocation, fileID.ToString() + ".docx"));
            document.Close();
            application.Quit();

            var file = new File()
            {
                ID = fileID.ToString(),
                FileUrl = Path.Combine(Properties.Settings.Default.FileDropLocation, fileID.ToString() + ".docx").ToString(),
                VisitorID = id
            };
            using (var db = new VisitorRegistrationEntities())
            {
                db.Files.Add(file);
                db.SaveChanges();
            }

            return fileID;
        }

        /// <summary>
        /// Starts the Download of the Visitor Pass
        /// </summary>
        /// <param name="fileID">File ID</param>
        /// <returns>Returns the file as <see cref="byte"/> Array</returns>
        public byte[] DownloadVisitorPass(Guid fileID)
        {
            var fileEntry = new Wcf.File();
            byte[] fileBinary = null;
            using (var db = new VisitorRegistrationEntities())
            {
                var f = from v in db.Files where v.ID == fileID.ToString() select v;

                fileEntry = f.FirstOrDefault();
                fileBinary = System.IO.File.ReadAllBytes(Path.Combine(Properties.Settings.Default.FileDropLocation, fileEntry.FileUrl));
            }

            using (var db = new VisitorRegistrationEntities())
            {
                int.TryParse(fileEntry.VisitorID.ToString(), out int visitorID);
                var visitor = (from v in db.Visitors
                            where v.ID == visitorID  
                            select v).FirstOrDefault();

                visitor.Printed  = true;

                db.SaveChanges();
            }

            return fileBinary;
        }
    }
}
