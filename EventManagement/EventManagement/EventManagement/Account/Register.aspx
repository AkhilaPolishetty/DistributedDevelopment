<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EventManagement.Account.Register" %>

<%@ Register Src="~/UserControl/SemesterDetails.ascx" TagPrefix="cse" TagName="semester" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
  <%--  <asp:FormView ID="Form1" runat="server"><cse:Semester runat="server"/></asp:FormView>--%>
        <h2 id ="RegisterPageId" runat ="server"></h2>
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">  
             <p><b>Provider:</b> Akhila<br />
                <b>Functionality Name:</b> Register Page with form validation<br />
                <b>Input:</b>Name, email, phone number,  password, re-captcha<br />
                <b>Output:</b>  Including the entry in database (XML file)<br />
                <b>Functionality Description:</b> >Service is used to validate the forms entered by user while registering and has a recaptcha(Image Service) to verifies if the user is human.<br /> It only
          allows the user to register on entering a unique mail ID. <br/>
          It uses XML manipulation to store results in the user/staff XML file <br /><br /><br />
            </p>

            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control"  />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
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
            <asp:Label runat="server" AssociatedControlID="Phone" CssClass="col-md-2 control-label">Phone</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Phone" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Phone"
                    CssClass="text-danger" ErrorMessage="The Phone field is required." />
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
         <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
               <h5>Image verifier service</h5><br /><br />
                <div class="g-recaptcha" data-sitekey="6LcoaTUUAAAAAMIrHRsXwWq2SznEpjjL-c8pYXe0">
                </div>
                <script src="https://www.google.com/recaptcha/api.js" type="text/javascript">
            </script><br />
            </div>
        </div>


        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>

        <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Label ID="ResultRegister" runat="server"></asp:Label>
            </div>
        </div>
    </div>
     
     <p><b>Provider:</b> Akhila<br />
        <b>Functionality Name:</b> User control<br />
        <b>Functionality Description:</b> User control is implemented to add a footer to every page about which semster and which  course this assignment is related to<br />
      </p>
    <div class="col-md-4">
            <section id="socialLoginForm">
                <cse:semester runat="server" id="SemesterDetailsFragment" />
            </section>
        </div>
</asp:Content>
