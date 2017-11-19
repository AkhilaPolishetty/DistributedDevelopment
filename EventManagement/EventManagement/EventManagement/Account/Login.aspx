<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EventManagement.Account.Login" Async="true" %>
<%@ Register Src="~/UserControl/SemesterDetails.ascx" TagPrefix="cse" TagName="semester" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h2 id ="LoginPageId" runat ="server"></h2>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <p ><b>Provider:</b> Akhila<br />
                           <b>Functionality Name:</b> Login Page<br />
                           <b>Input:</b> User email and user password<br />
                           <b>Output:</b>  On success user is redirected to userFeed page on Failure errors are displayed<br />
                           <b>Functionality Description:</b> This page takes the session values from the default page and displays login page for member/staff accordingly<br /><br /><br />
                        </p>
                        <asp:Label runat="server" AssociatedControlID="userEmail" CssClass="col-md-2 control-label">UserEmail</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="userEmail" CssClass="form-control" TextMode="Email"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="userEmail"
                                CssClass="text-danger" ErrorMessage="The user email field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:checkbox runat="server" id="rememberme" />
                                <asp:label runat="server" associatedcontrolid="rememberme">remember me?</asp:label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Label ID="ResultLogin" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <p> <b>This below label redirects you to the register page </b></p>
                 <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                    if you don't have a local account.
                </p>
            </section>
            
        </div>
       
        <p>
        <b>Provider:</b> Akhila<br />
        <b>Functionality Name:</b> User control<br />
        <b>Functionality Description:</b> User control is implemented to add a footer to every page about which semster and which  course this assignment is related to<br />
      </p>
         <div class="col-md-4">
            <section id="socialLoginForm">
                <cse:semester runat="server" id="SemesterDetailsFragmentLogin" />
            </section>
        </div> 
        </div>
        <div class="col-md-4">  
             <%--<p><b>Provider:</b> Akhila<br />
                           <b>Functionality Name:</b> Global asax<br />
                           <b>Functionality Description:</b> This feature is used in the application to print the details of the TA admin login only on the admin page not on the member page<br /><br /><br />
                        </p>--%>
                 <asp:label runat="server" ID ="ProviderLabel"></asp:label><br />
                 <asp:label runat="server" ID ="AdminUserName"></asp:label><br />
                 <asp:label runat="server" ID ="AdminPassword"></asp:label><br />
        </div>
       
    
</asp:Content>
