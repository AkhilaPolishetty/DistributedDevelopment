<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffPages.aspx.cs" Inherits="EventManagement.UserRoles.StaffPages.Admin" %>

<%@ Register Src="~/UserControl/SemesterDetails.ascx" TagPrefix="cse" TagName="semester" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Admin Event Handling</h1>
    <br />
    <div>
    <asp:Label ID="ResultLoginAdmin" runat="server"></asp:Label>
        </div>
    <div >
            <section id="socialLoginForm">
                <cse:semester runat="server" ID="SemesterDetailsFragmentLogin" />
            </section>
        </div>
     <br />
    <div >
       
    <p><b>Provider:</b> Ravi Teja Pinnaka<br />
                <b>Functionality Name:</b> Admin Login page with the abilities to Create Events, Delete Events, Enable and Disable Admins<br />
                <b>Input:</b> Admin Login Details like Username and Password<br />
                <b>Output:</b>  Creation of events, editing and managing the users on the application <br />
                <b>Functionality Description:</b> Admin Login page with the abilities to Create Events, Delete Events, Enable and Disable Admins <br/>
          It uses XML manipulation to store results in the user/staff XML file <br /><br /><br />
            </p>
        </div>
    <asp:Panel ID="Panel1" runat="server">
        <h3>Add Event</h3>
        <br />
        Please type the decription of event in the box<br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="98px" Width="461px"></asp:TextBox><br />
        Select date of the event
        <br />
        <br />
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Event" OnClick="Button1_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <h3>List of Events</h3>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Show all Events" OnClientClick="return loadXMLDoc();"/>
        <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="Black" 
    BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"></asp:Table>
        <h2>Delete Events</h2>
        <br />
        <br />
        Enter eventID to delete (Click on "Show all Events" and get the eventID)
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Delete Event" OnClick="Button3_Click" /><br />
        <br />
        <h2>Display of existing users</h2>
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Text="Show all users" OnClientClick="return puttext();" />

        <asp:Table ID="Table2" runat="server" BackColor="White" BorderColor="Black" 
    BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"></asp:Table>

        <br />
        <br />
        <h2>Disable/Enable Clients</h2>
        <br />
        <br />
        Enter user email to disable
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="Button4" runat="server" Text="Disable Client" OnClick="Button4_Click" />
        <br />
        <br />
        Enter user email to re-enable
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="Button6" runat="server" Text="Re-enable Client" OnClick="Button6_Click" />
        <script>
            function loadXMLDoc() {
                var xmlhttp = new XMLHttpRequest();
                xmlhttp.onreadystatechange = function () {
                    var data = xmlhttp.responseText;
                    //console.log('data', data);


                    if (this.readyState == 4 && this.status == 200) {

                        //console.log('this innerhtml', this.innerHTML);
                        //console.log('this response xml', this.responseXML)
                        //console.log('this child nodes', this.childNodes)
                        //console.log('this tag name', this.getElementsByTagName("event"));

                        myFunction(this);

                    }
                };
                xmlhttp.open("GET", "/XMLFiles/events.xml", true);
                //xmlhttp.overrideMimeType('application/xml');
                xmlhttp.send();
                return false;
            }
            function myFunction(xml) {
                console.log('xml passed  ', xml);
                var xmlDoc = xml.responseXML;
                table = "<tr style='border: 2px;padding : 2px'><th>Event Id    </th><th>Event    </th><th>Date          </th><th>Time</th></tr>";
                console.log('tab ', table);
                var x = xmlDoc.getElementsByTagName("event");
                console.log('x', x);
                console.log('x len', x.length);

                for (i = 0; i < x.length; i++) {
                    //console.log('d1 ', x[i].getElementsByTagName("data")[0].childNodes[0].nodeValue);
                    //alert('44');
                    var d0 = x[i].getElementsByTagName("eventID")[0].childNodes[0].nodeValue;
                    var d1 = x[i].getElementsByTagName("data")[0].childNodes[0].nodeValue;
                    var d2 = x[i].getElementsByTagName("date")[0].childNodes[0].nodeValue;
                    var d3 = x[i].getElementsByTagName("time")[0].childNodes[0].nodeValue;

                    //alert('55');
                    table += "<tr><td>" + d0 + "</td><td>" + d1 + "</td><td>" + d2 + "</td><td>" + d3 + "</td></tr>";
                    //@HTML.CheckBox("Person.HadDinner", Model.Person.HadDinner)
                    //+ '<input id="checkBox" type="checkbox"/>' 
                    //alert('55');
                    //console.log('d2 ', d2);
                    //alert('end',x[1].getElementsByTagName("data")[0].childNodes[0].nodeValue);
                }
                console.log('tab ' + table);
                table += " <style> table, th, td {  border: 1px solid black;} </style>";

                document.getElementById('<%= Table1.ClientID %>').innerHTML = table;

            }

            function puttext() {
                var xmlhttp = new XMLHttpRequest();
                xmlhttp.onreadystatechange = function () {
                    var data = xmlhttp.responseText;



                    if (this.readyState == 4 && this.status == 200) {

                        myFunction1(this);

                    }
                };
                xmlhttp.open("GET", "/XMLFiles/users.xml", true);

                xmlhttp.send();
                return false;
            }
            function myFunction1(xml) {
                var xmlDoc = xml.responseXML;
                table = "<tr><th>Name    </th><th>User Enabled    </th><th>Events subscribed          </th><th>Email     </th><th>Phone     </th></tr>";

                console.log('tab ', table);
                var x = xmlDoc.getElementsByTagName("user");
                console.log('x', x);
                console.log('x len', x.length);

                for (i = 0; i < x.length; i++) {

                    var d0 = x[i].getElementsByTagName("name")[0].childNodes[0].nodeValue;
                    var d1 = x[i].getElementsByTagName("enabled")[0].childNodes[0].nodeValue;
                    var d2 = x[i].getElementsByTagName("eventID")[0].childNodes[0].nodeValue;
                    var d3 = x[i].getElementsByTagName("email")[0].childNodes[0].nodeValue;
                    var d4 = x[i].getElementsByTagName("phone")[0].childNodes[0].nodeValue;


                    table += "<tr><td>" + d0 + "&emsp;</td><td>" + d1 + "&emsp;</td><td>" + d2 + "</td>&emsp;<td>" + d3 + "</td><td>" + d4 + "</td></tr>";

                }


                document.getElementById('<%= Table2.ClientID %>').innerHTML = table;

            }

        </script>
        <br />
        <br />
        <div class="form-group">
            <p ><b>Provider:</b> Akhila<br />
                <b>Functionality Name:</b> Add Another Admin<br />
                <b>Functionality Description:</b> This button lets as admin only to add another admin to the staff xml file<br /><br /><br />
            </p>
            </div>
        
        <div class="col-md-4">
            <asp:Button runat="server" OnClick="AddAnotherAdmin_Click" Text="Click to add another Admin" CssClass="btn btn-default" />
        </div>
           <br />
        <br />
        
        <div class="col-md-4">
            <asp:Button runat="server" OnClick="LogOut" Text="Log Out" CssClass="btn btn-default" />
        </div>
        

    </asp:Panel>



</asp:Content>