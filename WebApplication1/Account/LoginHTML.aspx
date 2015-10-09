<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginHTML.aspx.cs" Inherits="WebApplication1.Account.LoginHTML" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../Resources/CSS/site-collection.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body-container" class="auth-form">
        <div id="left">
            <div id="dpu-logo">
                <div id="dpu-footer">
					<p>© 2001-2015 DePaul University | <a href="http://www.depaul.edu/Pages/disclaimer.aspx">Disclaimer</a> | <a href="http://www.depaul.edu/Pages/contact-us.aspx">Contact</a><br>1 E. Jackson, Chicago, IL 60604 | 312-362-8000</p>
				</div>
            </div>
        </div>  


        <div id="right">
	        <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>
	 
	
	    </div>

    </div>


    </form>


</body>
</html>
