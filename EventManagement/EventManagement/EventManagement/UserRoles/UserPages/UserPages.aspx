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
        <asp:Panel ID="Panel2" runat="server">
            <h2>Show all events</h2> <br />
            <asp:Button ID="Button2" runat="server" Text="Show all events" OnClientClick="return loadXMLDoc();"/><br /><br />
            <asp:Table ID="Table2" runat="server"></asp:Table>
            <script>
                function loadXMLDoc() {
                    var xmlhttp = new XMLHttpRequest();
                    xmlhttp.onreadystatechange = function () {
                        var data = xmlhttp.responseText;
                        if (this.readyState == 4 && this.status == 200)
                            myFunction(this);
                    };
                    xmlhttp.open("GET", "/XMLFiles/events.xml", true);
                    xmlhttp.send();
                    return false;
                }
                function myFunction(xml) {
                    console.log('xml passed  ', xml);
                    var xmlDoc = xml.responseXML;
                    table = "<tr><th>Event Id    </th><th>Event    </th><th>Date          </th><th>Time</th></tr>";
                    console.log('tab ', table);
                    var x = xmlDoc.getElementsByTagName("event");
                    console.log('x', x);
                    console.log('x len', x.length);

                    for (i = 0; i < x.length; i++) {
                        var d0 = x[i].getElementsByTagName("eventID")[0].childNodes[0].nodeValue;
                        var d1 = x[i].getElementsByTagName("data")[0].childNodes[0].nodeValue;
                        var d2 = x[i].getElementsByTagName("date")[0].childNodes[0].nodeValue;
                        var d3 = x[i].getElementsByTagName("time")[0].childNodes[0].nodeValue;
                        table += "<tr><td>" + d0 + "</td><td>" + d1 + "</td><td>" + d2 + "</td><td>" + d3 + "</td></tr>";
                    }
                    document.getElementById('<%= Table2.ClientID %>').innerHTML = table;

            }
            </script>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server">


        </asp:Panel>
        <h2>Subscribe to an event</h2>
            <br />
        Enter Event ID : <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> <br /><br />
        
       <asp:Panel ID="Panel4" runat="server">
             <asp:Label ID="SmsLabel" runat="server" Text="Label">To send the event to your mobile enter your carrier</asp:Label><br />      
            <table>     
             <tr>
                <td class="auto-style2" style="width: 300px; height: 78px;">
                    <asp:Label ID="Label5" runat="server" Text="Enter Carrier GateWay"></asp:Label></td>
                <td style="height: 78px">
                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" Height="50px" Width="200px">
                         <asp:ListItem Text="Tmobile" Value="tmomail.net" />
                         <asp:ListItem Text="at&t" Value="txt.att.net" />
                         <asp:ListItem Text="Sprint" Value="messaging.sprintpcs.com" />
                         <asp:ListItem Text="Verizon Wireless" Value="vtext.com" />
                         <asp:ListItem Text="Virgin Mobile" Value="vmobl.com" />
                         <asp:ListItem Text="Alltel" Value="sms.alltelwireless.com" />
                         <asp:ListItem Text="Boost Mobile" Value="sms.myboostmobile.com" />
                         <asp:ListItem Text="Metro PCS" Value="mymetropcs.com" />
                         <asp:ListItem Text="Republic Wireless" Value="txt.republicwireless.com" />
                         
                    </asp:DropDownList>
                </td>
                   </tr>
           </table>
           

        <asp:Button ID="Button3" runat="server" Text="Subscribe !" OnClick="Subscribe_Click" /> <br />

       </asp:Panel>

        



        <asp:Panel ID="Panel1" runat="server">
             <h2>Events Subscribed</h2>
            <br />
 
            <asp:Button ID="Button1" runat="server" Text="Click to Show" OnClick="Show_events_subscribed" /><br /><br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            
        </asp:Panel>
        

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