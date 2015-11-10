<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Account.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="../Resources/Content/bootstrap.css" rel="stylesheet" type="text/css" />
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
	        <div class="box f1-panel" id="login">
               <div id="dpu-info" class="login-center">
							<h2>Campus Connect Registration</h2>
							<div id="dpu-LoginFormInstructions">
								<p>Please fill out the information below.</p>
							</div>
				</div>
            </div>
	        <div id="form-container" class="login-center">
					
	<div id="dpu-LoginFormInner">
		<fieldset class="inlineLabels">
                <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
			        <%-- <asp:ValidationSummary runat="server" CssClass="text-danger" />--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DepaulId" CssClass="col-md-2 control-label">DePaul Id</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="DepaulId" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="DepaulId"
                    CssClass="text-danger" ErrorMessage="The DePaul Id field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
		</fieldset>	
      </div>
				
		<div class="buttonHolder ctrlHolder"> 
			<input type="hidden" name="_eventId" value="submit"/>
        
			&nbsp;<asp:Button ID="RegisterBtn" runat="server" Text="   Register   " OnClick="CreateUser_Click" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small"/>
 
		</div>
         </div>   
              <p>
                  <a href="/Account/Login">Already registered? login here</a>
              </p>
		</div>
	</div>
	<div class="clear"></div>
</div>
	
	    </div>

    
    </form>

    
</body>
</html>
