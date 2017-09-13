<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VisitorRegistration.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link href="Content/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" type="text/css" rel="stylesheet" />
    <link href="Content/custom.min.css" type="text/css" rel="stylesheet" />
    <title>Login Dialog</title>
</head>
<body>

    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <a class="navbar-brand" href="default.aspx" runat="server">Visitor Registration</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarCollapse">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link" href="#" onserverclick="EN_ServerClick" runat="server">
                        <asp:Localize runat="server"
                            ID="loc_English"
                            Text="English" meta:resourcekey="English" /><span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#" onserverclick="DE_ServerClick" runat="server">
                        <asp:Localize runat="server"
                            ID="loc_german"
                            Text="German" meta:resourcekey="German" /></a>
                </li>

            </ul>

        </div>
    </nav>
    <div class="container">
        <div class="content">
            <h4>
                <asp:Localize runat="server"
                    ID="loc_welcometext"
                    Text="Please enter your Informations below." meta:resourcekey="Welcome" /></h4>
            <br />
            <form runat="server" id="submitform" method="post">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label runat="server" for="inputFirstName" class="col-form-label">
                            <asp:Localize runat="server"
                                ID="loc_inputFirstName"
                                Text="First Name" meta:resourcekey="FirstName" /></label>
                        <input runat="server" type="text" class="form-control" id="inputFirstName" placeholder="" />
                    </div>
                    <div class="form-group col-md-6">
                        <label runat="server" for="inputLastName" class="col-form-label">
                            <asp:Localize runat="server"
                                ID="loc_inputLastName"
                                Text="Last Name" meta:resourcekey="LastName" /></label>
                        <input runat="server" type="text" class="form-control" id="inputLastName" placeholder="" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputID" runat="server" class="col-form-label">
                        <asp:Localize runat="server"
                            ID="loc_inputID"
                            Text="Identity Number" meta:resourcekey="IdentityNumber" /></label>
                    <input type="text" runat="server" class="form-control" id="inputID" placeholder="1234" />
                </div>
                <div class="form-group">
                    <label for="inputEmail" runat="server" class="col-form-label">
                        <asp:Localize runat="server"
                            ID="loc_Email"
                            Text="E-Mail" meta:resourcekey="Email" /></label>
                    <input type="email" runat="server" class="form-control" id="inputEmail" placeholder="Email" />
                </div>
                <div class="form-group">
                    <label for="inputHostEmail" runat="server" class="col-form-label">
                        <asp:Localize runat="server"
                            ID="loc_hostemail"
                            Text="E-Mail of your Host" meta:resourcekey="EmailOfHost" /></label>
                    <input type="email" runat="server" class="form-control" id="inputHostEmail" placeholder="Email" />

                </div>



                <div class="container">
                    <div id="signature-pad" class="m-signature-pad">
                        <div class="m-signature-pad--body">
                            <canvas width="658" runat="server" height="318" style="touch-action: none;"></canvas>
                        </div>
                        <div class="m-signature-pad--footer">
                            <div class="description">
                                <asp:Localize runat="server"
                                    ID="loc_signabove"
                                    Text="Sign above" meta:resourcekey="Signabove" />
                            </div>
                            <button type="button" class="button clear" runat="server" data-action="clear">
                                <asp:Localize runat="server"
                                    ID="loc_clear"
                                    Text="Clear" meta:resourcekey="Clear" /></button>

                        </div>
                    </div>
                </div>
                <button type="submit" runat="server" onserverclick="SignIn_ServerClick" style="display: none" id="hiddenbutton"></button>
                <button type="button" class="btn btn-primary" runat="server" onclick="save();">
                    <a class="fa fa-save"></a>
                    <asp:Localize runat="server"
                        ID="loc_signin"
                        Text="Sign In" meta:resourcekey="SignIn" /></button>
                <button type="button" class="btn btn-danger" runat="server" onclick="document.getElementById('submitform').reset();">
                    <a class="fa fa-times"></a>
                    <asp:Localize runat="server"
                        ID="loc_clearall"
                        Text="Clear all" meta:resourcekey="ClearAll" /></button>
                <input id="signaturebinary" style="display: none;" runat="server" value="" type="text" />
            </form>


        </div>
    </div>

    <footer class="footer">
        <div class="container">
            <span class="text-muted">Copyright &copy; <% Response.Write(DateTime.Now.Year); %> Julian Hüppauff.</span>
        </div>
    </footer>





    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="Content/bootstrap.min.css" type="text/javascript"></script>
    <script src="Scripts/signature_pad.min.js" type="text/javascript"></script>
    <script src="Scripts/app.min.js" type="text/javascript"></script>
    <script src="Scripts/popper.min.js" type="text/javascript"></script>
</body>
</html>
