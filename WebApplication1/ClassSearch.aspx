<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassSearch.aspx.cs" Inherits="WebApplication1.ClassSearch" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


     <div class="jumbotron" >
        <section id="calculator-app" >
            <div id="calc-tabs" >
                DePaul When If
                <ul>
                  <li><a href="DegreeProgress">Degree Progress</a></li>
                  <li><a href="Calculator">Calculator</a></li>
                  <li><a href="ClassSearch">ClassSearch</a></li>
                </ul>
            </div>
             <div id="calc-title" >
                Class Search

            </div>
            <div id="calc-results" >
                Search for classes and find out more about them <br>
                <asp:TextBox runat="server" ID="searchString" CssClass="form-control" TextMode="SingleLine" />
               
                &nbsp;&nbsp;&nbsp;<asp:Button ID="searchBtn" runat="server" Text="   Search   " OnClick="ClassSearchUrl" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;"/>
                <br  />
                <asp:ListBox name="resultBox" ID="resultsBox" runat="server" CssClass="resultBox" OnSelectedIndexChanged="RedirectUrl" style=" width: 60%; height: 60%;  margin: auto;" >
                </asp:ListBox><br />
                <br />
               
            </div>
        </section>
    </div>
</asp:Content>
