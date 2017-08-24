using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VisitorRegistration.Wcf
{
    [ServiceContract]
    public interface IEndpoint
    {
        [OperationContract]
        void RegisterVisitor(string FirstName, string LastName, string Email, string HostEmail, string IDNumber, string signature);
        [OperationContract]
        Visitor GetVisitor(int ID);
        [OperationContract]
        List<Visitor> GetCurrentVisitors();
        [OperationContract]
        byte[] DownloadVisitorPass(Guid ID);
        [OperationContract]
        Guid GenerateVisitorPass(int ID);
    }
}
