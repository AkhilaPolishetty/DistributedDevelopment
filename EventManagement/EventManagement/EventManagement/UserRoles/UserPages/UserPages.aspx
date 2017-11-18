<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPages.aspx.cs" Inherits="EventManagement.UserRoles.UserPages.UserPages" %>
<%@ Register Src="~/UserControl/SemesterDetails.ascx" TagPrefix="cse" TagName="semester" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:Label ID="ResultLogin" runat="server"></asp:Label>
    </div>
         <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="LogOut" Text="Log Out" CssClass="btn btn-default" />
            </div>
        </div>
               
       <div class="col-md-4">
            <section id="socialLoginForm">
                <cse:semester runat="server" id="SemesterDetailsFragmentLogin" />
            </section>
        </div> 
    </form>


</body>
</html>