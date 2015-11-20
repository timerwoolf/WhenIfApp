<%@ Page Title="Degree Progress" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DegreeProgress.aspx.cs" Inherits="WebApplication1.DegreeProgress" %>

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
                Degree Progress

            </div>
            <div id="calc-results" >
                Search for classes and add them to your degree <br>
                <asp:TextBox runat="server" ID="searchString" CssClass="form-control" TextMode="SingleLine" />
               
                &nbsp;&nbsp;&nbsp;<asp:Button ID="searchBtn" runat="server" Text="   Search   " OnClick="ClassSearch" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;"/>
                <br  />
                <asp:ListBox name="resultBox" ID="resultsBox" runat="server" CssClass="resultBox" style=" width: 60%;  margin: auto;" >
                </asp:ListBox><br />
                <br />
               &nbsp;&nbsp;&nbsp;<asp:Button ID="Add" runat="server" Text=" Add Class " OnClick="addClass" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;" />
               <br  />
                 <asp:ListBox ID="savedBox" runat="server" CssClass="resultBox" style=" width: 60%;  margin: auto; float: left; display:inline;" >
                </asp:ListBox><br />    
                <br />   
                &nbsp;&nbsp;&nbsp;<asp:Button ID="DeleteBtn" runat="server" Text=" Delete Class " OnClick="deleteClass" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;" />        
               &nbsp;&nbsp;&nbsp;<asp:Button ID="SaveBtn" runat="server" Text=" Save Degree " OnClick="saveClasses" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small" style=" left:50%;" />
                
            </div>
        </section>
    </div>


</asp:Content>
