<%@ Page Title="Calculator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="WebApplication1.Calculator" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <section id="calculator-app">
            <div id="calc-tabs">
                TABS
                <ul>
                  <li><a href="DegreeProgress">Degree Progress</a></li>
                  <li><a href="Calculator">Calculator</a></li>
                  <li><a href="#contact">Contact</a></li>
                  <li><a href="#about">About</a></li>
                </ul>
            </div>
             <div id="calc-title">
                RESULTS

            </div>
            <div id="calc-results">
                <iframe name="myIframe" src="~/PageSearch/Search" id="myIframe" height="400px" runat =server style="width: 278px"></iframe>

            </div>
        </section>
    </div>


</asp:Content>
