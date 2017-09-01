using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VisitorRegistration.Wcf
{
    public class Endpoint : IEndpoint
    {
        public void RegisterVisitor(string FirstName, string LastName, string Email, string HostEmail, string IDNumber, string signature)
        {
            var Visitor = new Visitor()
            {
                DateOfVisit = DateTime.Now,
                Email = Email,
                Last_Name = LastName,
                First_Name = FirstName,
                IDNumber = IDNumber,
                Printed = false,
                HostEmail = HostEmail,
                Signature = signature

            };
            using (var db = new VisitorRegistrationEntities())
            {
                db.Visitors.Add(Visitor);
                db.SaveChanges();
            }
        }

        public Visitor GetVisitor(int ID)
        {
            using (var db = new VisitorRegistrationEntities())
            {
                var visitor = from v in db.Visitors where v.ID == ID select v;

                Visitor VisitorEntry = visitor.FirstOrDefault();
                return VisitorEntry;
            }

        }

        public List<Visitor> GetCurrentVisitors()
        {
            using (var db = new VisitorRegistrationEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var visitor = from v in db.Visitors where v.DateOfVisit == DateTime.Today select v;

                List<Visitor> VisitorEntrys = new List<Visitor>();
                foreach (Visitor VisitorEntry in visitor)
                {
                    VisitorEntry.Signature = null;
                    VisitorEntrys.Add(VisitorEntry);
                }


                return VisitorEntrys;
            }
        }

        public Guid GenerateVisitorPass(int ID)
        {
            Visitor visitor = GetVisitor(ID);
            Guid FileID = Guid.NewGuid();

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
                else if(field.Code.Text.Contains("DateOfVisit"))
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

            document.SaveAs2(FileName: Path.Combine(Properties.Settings.Default.FileDropLocation, FileID.ToString()+ ".docx"));
            document.Close();
            application.Quit();

            var File = new File()
            {
                ID = FileID.ToString(),
                FileUrl = Path.Combine(Properties.Settings.Default.FileDropLocation, FileID.ToString() + ".docx").ToString(),
                VisitorID = ID
            };
            using (var db = new VisitorRegistrationEntities())
            {
                db.Files.Add(File);
                db.SaveChanges();
            }

            return FileID;
        }

        public byte[] DownloadVisitorPass(Guid FileID)
        {
            var FileEntry = new Wcf.File();
            byte[] FileBinary = null;
            using (var db = new VisitorRegistrationEntities())
            {
                var F = from v in db.Files where v.ID == FileID.ToString() select v;

                FileEntry = F.FirstOrDefault();


                
                FileBinary = System.IO.File.ReadAllBytes(Path.Combine(Properties.Settings.Default.FileDropLocation, FileEntry.FileUrl));
            }

            using (var db = new VisitorRegistrationEntities())
            {
                int VisitorID = 0;
                int.TryParse(FileEntry.VisitorID.ToString(), out VisitorID);
                var visitor = (from v in db.Visitors
                            where v.ID == VisitorID  
                            select v).FirstOrDefault();

                visitor.Printed  = true;

                db.SaveChanges();
            }

            return FileBinary;
        }
    }
}
