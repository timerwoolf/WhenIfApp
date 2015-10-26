<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebApplication1.PageSearch.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <style type="text/css">


    .btn-submit, .btn-reset
{
    font-size: 13px!important;
	font-weight: normal!important;
	width: 117px;
	height: 26px;
	color: #fff;
	font-weight: bold;
	background: #74cd00; /* Old browsers */
	background: -moz-linear-gradient(top, #74cd00 0%, #4c8700 100%); /* FF3.6+ */
	background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#74cd00), color-stop(100%,#4c8700)); /* Chrome,Safari4+ */
	background: -webkit-linear-gradient(top, #74cd00 0%,#4c8700 100%); /* Chrome10+,Safari5.1+ */
	background: -o-linear-gradient(top, #74cd00 0%,#4c8700 100%); /* Opera11.10+ */
	background: -ms-linear-gradient(top, #74cd00 0%,#4c8700 100%); /* IE10+ */
	filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#74cd00', endColorstr='#4c8700',GradientType=0 ); /* IE6-9 */
	background: linear-gradient(top, #74cd00 0%,#4c8700 100%); /* W3C */
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
        <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
        <p>
            <asp:Button ID="RegisterBtn" runat="server" Text="   run Search   " OnClick="run_search" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small"/>
 
            <asp:Button ID="RegisterBtn0" runat="server" Text="   testrun   " OnClick="test_run" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small"/>
 
		</p>
        <p>
            © 2001-2015 DePaul University | <a href="http://www.depaul.edu/Pages/disclaimer.aspx">Disclaimer</a> | <a href="http://www.depaul.edu/Pages/contact-us.aspx">Contact</a><br>1 E. Jackson, Chicago, IL 60604 | 312-362-8000</p>
    </form>
    </body>
</html>
