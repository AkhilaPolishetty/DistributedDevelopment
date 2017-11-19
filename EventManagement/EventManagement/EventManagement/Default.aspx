<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventManagement._Default"
     %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p ><b>Provider:</b> Akhila<br />
        <b>Functionality Name:</b> Default Page<br />
        <b>Functionality Description:</b> This page has controls to user login, user registration, admin login pages<br /><br /><br />
    </p>
    </div>
    <div class="jumbotron" style=" font-family: Helvetica Neue, Helvetica, Arial, sans-serif; opacity:0.8; filter:alpha(opacity=50); text-decoration: none; color: #dcd9d9; margin-left: auto; margin-right: auto; text-align: center;
                                    background-image:url(Images/IndexImage.jpg); background-color: transparent;" >
        <h1 style="margin-left: 10px">Event Board Application</h1>
        
        <p style="margin-left: 10px"><b>Application provides two user roles - Admin and Member</b></p>
        
        <p font-family: "Helvetica Neue" class="lead" style="margin-left: 10px; margin-right: auto; text-align: left; width: 2000px;">
            <b>Member Capabilities of Event Board</b>  
            <br />
            It allows user to add events from the default events. <br />  
            It allows you to add events to your personal Event Board.  <br />  
            It provides a 'Subscribe and send event to Mobile' option to send a particular event to you mobile phone.<br />  <br />  
        </p>


       <p>This button redirects the member to the member Login page</p>
       <asp:Button runat="server" ID="UserLogin" OnClick="LoginUser_Click" Text="User Sign in" CssClass="btn btn-default" style="margin-left: 50px"/>  <br />  
       <p>This button redirects the member to the member Register page</p>
       <asp:Button runat="server" OnClick="CreateUser_Click" Text="User Register" CssClass="btn btn-default" style="margin-left: 50px"/> <br />

        <p class="lead" style="margin-left: 10px; margin-right: auto; text-align: left; width: 2000px;" font-family: "Helvetica Neue">
            <b>Admin  Capabilities of Event Board</b>  <br />
            Staff can disable and enable an user.<br />
            Staff can add events to the event board. <br />  
            Staff can delete events from the event board.</p>
        
       <p>This button redirects the Admin to the Admin Login page</p>
       <asp:Button runat="server" OnClick="LoginAdmin_Click" Text="Admin Sign in" CssClass="btn btn-default" style="margin-left: 50px"/>  <br />    
      <%-- <asp:Button runat="server" OnClick="CreateAdmin_Click" Text="Admin Register" CssClass="btn btn-default" style="margin-left: 50px"/>
         --%>
    </div>
    
    

</asp:Content>
