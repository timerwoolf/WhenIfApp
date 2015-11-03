<%@ Page Title="Degree Progress" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DegreeProgress.aspx.cs" Inherits="WebApplication1.DegreeProgress" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <section id="calculator-app">
            <div id="calc-tabs">
                DePaul When If
                <ul>
                  <li><a href="DegreeProgress">Degree Progress</a></li>
                  <li><a href="Calculator">Calculator</a></li>
                </ul>
            </div>
             <div id="calc-title">
                Degree Progress

            </div>
            <div id="calc-results">
                Functions and list of classes goes here <br>
                <asp:TextBox runat="server" ID="searchString" CssClass="form-control" TextMode="SingleLine" />
                  <asp:RequiredFieldValidator runat="server" ControlToValidate="searchString"
                    CssClass="text-danger" ErrorMessage="The search field is required." />
                <asp:TextBox runat="server" ID="resultBox" CssClass="form-control" TextMode="Search" readonly="true" />
                &nbsp;<asp:Button ID="searchBtn" runat="server" Text="   Search   " OnClick="ClassSearch" BackColor="#70C600" CssClass="btn-submit" Font-Bold="True" Font-Size="Small"/>
 
            </div>
        </section>
    </div>


</asp:Content>
