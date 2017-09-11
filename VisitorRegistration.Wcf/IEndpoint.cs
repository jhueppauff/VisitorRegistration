//-----------------------------------------------------------------------
// <copyright file="IEndpoint.cs" company="None">
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
    using System.ServiceModel;

    /// <summary>
    /// IEndpoint for the WCF <see cref="Endpoint"/> Class
    /// </summary>
    [ServiceContract]
    public interface IEndpoint
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
        [OperationContract]
        void RegisterVisitor(string firstName, string lastName, string email, string hostEmail, string idNumber, string signature);

        /// <summary>
        /// Get Visitor by ID
        /// </summary>
        /// <param name="id">ID of the Visitor</param>
        /// <returns><see cref="Visitor"/></returns>
        [OperationContract]
        Visitor GetVisitor(int id);

        /// <summary>
        /// Get a List of the type<see cref="Visitor"/> from the daily visitors
        /// </summary>
        /// <returns>Returns a list of the daily <see cref="Visitor"/></returns>
        [OperationContract]
        List<Visitor> GetCurrentVisitors();

        /// <summary>
        /// Starts the Download of the Visitor Pass
        /// </summary>
        /// <param name="fileID">File ID</param>
        /// <returns><see cref="Returns File as Byte"/></returns>
        [OperationContract]
        byte[] DownloadVisitorPass(Guid fileID);

        /// <summary>
        /// Generates the Visitor Pass
        /// </summary>
        /// <param name="id">Visitor ID</param>
        /// <returns>Returns the FileID as <see cref="Guid"/></returns>
        [OperationContract]
        Guid GenerateVisitorPass(int id);
    }
}
