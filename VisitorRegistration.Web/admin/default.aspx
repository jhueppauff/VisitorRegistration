<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="VisitorRegistration.Web.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="../Content/custom.min.css" type="text/css" rel="stylesheet" />
    <link href="../Content/font-awesome.min.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <a class="navbar-brand" href="default.aspx" runat="server">Visitor Registration</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

    </nav>

    <div class="container">
        <div class="content">
            <div class="container">

                <table class="table">
                    <thead>
                        <tr>

                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Host</th>
                            <th>ID</th>
                            <th>Print</th>
                        </tr>
                    </thead>
                    <tbody runat="server" id="tbody">
                    </tbody>
                </table>
            </div>
        </div>
    </div>



    <footer class="footer">
        <div class="container">
            <span class="text-muted">Copyright &copy; <% Response.Write(DateTime.Now.Year); %> Julian Hüppauff.</span>
        </div>
    </footer>
    <script src="../Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="../Content/bootstrap.min.css" type="text/javascript"></script>
    <script src="../Scripts/popper.min.js" type="text/javascript"></script>




    <script type="text/javascript">
        function PrintPass(ID) {
           
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "default.aspx/PrintVisitor",
                    data: "{ ID: "+ID+" }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(location).attr('href',  "download.aspx?id=" + response.d);
                    },
                    failure: function (response) {
                        
                    }
                });
            
        }
    </script>
  
</body>
</html>
