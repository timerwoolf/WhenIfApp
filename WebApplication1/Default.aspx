<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>When if Calculator</h1>
        <p class="lead">Welcome Future graduates of depaul.  On this website you can calculate how long it will take for you to obtain your degree
            given a specific set conditions
        </p>
        <div id="faculty" runat="server"><a runat="server" href="~/Faculty">Faculty Services</a>
        </div>

    </div>

   

</asp:Content>

