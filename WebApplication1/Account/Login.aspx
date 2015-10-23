<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Account.Login" %>

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
	        <div class="box f1-panel" id="login">
               <div id="dpu-info" class="login-center">
							<h2>Campus Connect Authentication</h2>
							<div id="dpu-LoginFormInstructions">
								<p>Please enter your Campus Connect User ID and Password</p>
							</div>
				</div>
            </div>
	        <div id="form-container" class="login-center">
					
	<div id="dpu-LoginFormInner">
		<fieldset class="inlineLabels">
			<div class="ctrlHolder"> 
				<label class="fixed-width" for="username"><span class="accesskey">U</span>ser ID:</label>
								
				 <asp:TextBox runat="server" ID="UserName" CssClass="auth-form" TextMode="SingleLine" Height="25px" Width="220px" />
					<div id="xuser" class="xicon" style="display:none"></div>
			</div>
			<div class="ctrlHolder"> 
				<label class="fixed-width" for="password"><span class="accesskey">P</span>assword:</label>
					<div><asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Height="25px" Width="220px" /></div>			
					
                    <div><asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." /></div>
             </div>
		</fieldset>	
      </div>
				
		<div class="buttonHolder ctrlHolder"> 
			<input type="hidden" name="_eventId" value="submit"/>
        
			&nbsp;<asp:Button ID="LoginBtn" runat="server" Text="   Login   " OnClick="LogIn_user" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small"/>
            <div class="checkbox">
               <asp:CheckBox runat="server" ID="RememberMe" />
               <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
            </div>   
              <p>
                  <a href="/Account/Register">Register as new user</a>
              </p>
		</div>
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
	</div>
	<div class="clear"></div>
</div>
	
	    </div>

    
    </form>

    
</body>
</html>
