<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Faculty.aspx.cs" Inherits="WebApplication1.Faculty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="jumbotron" >
        <section id="calculator-app" >
            <div id="calc-tabs" >
                DePaul When If
                <ul>
                  <li><a href="Faculty">Student Search</a></li>
                  
                </ul>
            </div>
             <div id="calc-title" >
                Student Search

            </div>
            <div id="calc-results" >
                Search for available students <br>
                <asp:TextBox runat="server" ID="searchString" CssClass="form-control" TextMode="SingleLine" />
               
                &nbsp;&nbsp;&nbsp;<asp:Button ID="searchBtn" runat="server" Text="   Search   " OnClick="StudentSearch" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;"/>
                <br  />
                <asp:ListBox name="savedBox" ID="savedBox" runat="server" CssClass="resultBox" style=" width: 60%;  margin: auto;" >
                </asp:ListBox><br />
                <br />
               <br  />
           
            </div>
        </section>
    </div>






</asp:Content>


