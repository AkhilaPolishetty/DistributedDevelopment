using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace EventManagement.UserRoles.UserPages
{
    public partial class UserPages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Stop Caching in IE
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
            if (Session["SessionVar"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            if ((CookieVar == null) || (CookieVar["UserSessionCookie"] == ""))
            {
                ResultLogin.Text = "Welcome New User!";
            }
            else
            {
                ResultLogin.Text = "<h2>"+"Welcome " + CookieVar["UserSessionCookie"] + "!" + "</h2>";
            }

        }



        protected void LogOut(object sender, EventArgs e)
        {
            //Loggin the user out and deleting users session variables 
            if (Request.Cookies["UserSessionCookie"] != null)
            {
                HttpCookie CookieKill = new HttpCookie("UserSessionCookie");
                CookieKill.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(CookieKill);
            }
            Session["SessionVar"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {

            //If the user wants to subscribe to an event 
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            string message = "";
            string usermail = CookieVar["UserSessionCookie"];
          
            if (TextBox1.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Details empty')", true);
                return;
            }
            
            string App_Data_Path;

            bool exists = false;
            try
            {
                //Get the data of the events to add it to users list
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"events.xml");
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList xnList = xmldoc.SelectNodes("/eventDetails");
                    foreach (XmlNode xn in xnList)
                    {
                        XmlNodeList CNodes = xn.SelectNodes("event");
                        foreach (XmlNode node in CNodes)
                        {
                            if (TextBox1.Text == node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty))
                            {
                                message = node["data"].InnerText.Trim().Replace("\r\n", string.Empty);
                                subscribeEvent(message);
                                exists = true;
                                break;
                            }
                        }
                        if (exists == true)
                            break;
                    }
                }
                else
                    Console.WriteLine("File doesn't exist \n");
                if (!exists)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage1", "alert('EventID doesnt exist')", true);
                
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
            
        }
        void subscribeEvent(string message)
        {
            //SMS service reference object to send message to the user once he subscribes to the event 
            SMSServiceReference.Service1Client obj = new SMSServiceReference.Service1Client();
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            string usermail = CookieVar["UserSessionCookie"], App_Data_Path;
            string subject = SubjectBox.Text;
            //Removing stop words from subject to give a crisper text
            WordFilterServiceReference.Service1Client serv = new WordFilterServiceReference.Service1Client();
            string clearSubject = serv.RemoveStopWords(subject);

            long mobileNumber = 0;
            string gateWay = DropDownList1.SelectedItem.Value; 
            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList xnList = xmldoc.SelectNodes("/UserDetails");
                    foreach (XmlNode xn in xnList)
                    {
                        XmlNodeList CNodes = xn.SelectNodes("user");
                        foreach (XmlNode node in CNodes)
                        {
                            if (usermail == node["email"].InnerText.Trim().Replace("\r\n", string.Empty))
                            {
                                string[] split_ids = node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty).Split(',');
                                if (!split_ids.Contains(TextBox1.Text))
                                {
                                    node["eventID"].InnerText += "," + TextBox1.Text;
                                    xmldoc.Save(Path.Combine(App_Data_Path, @"users.xml"));
                                    //Checking if the gateway input is not null
                                    if (!String.IsNullOrWhiteSpace(gateWay))
                                    {
                                        mobileNumber = Convert.ToInt64(node["phone"].InnerText.Trim().Replace("\r\n", string.Empty));
                                        //Calling the SMS function with required details
                                        string result = obj.SMSservice(clearSubject, message, mobileNumber, gateWay);
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Subscribed and message sent')", true);
                                    }
                                    else
                                    {
                                        //Not sending a message if the gateway is null
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Subscribed and but message not sent')", true);
                                    }

                                    break;
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Already Subscribed')", true);
                                }
                                
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("File doesn't exist \n");
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        protected void Show_events_subscribed(object sender, EventArgs e)
        {
           
            bool flag = false;
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            string usermail = CookieVar["UserSessionCookie"], App_Data_Path,table="123";

            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
            
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList xnList = xmldoc.SelectNodes("/UserDetails");
                    table = "<table style='border: 2px;padding : 2px' ><tr><th>Event Id    </th><th>Event    </th><th>Date          </th><th>Time</th></tr>";
                    foreach (XmlNode xn in xnList)
                    {
                        XmlNodeList CNodes = xn.SelectNodes("user");
                        
                        foreach (XmlNode node in CNodes)
                        {
                            //If the users email matches 
                            if (usermail == node["email"].InnerText.Trim().Replace("\r\n", string.Empty))
                            {
                                string[] eventids= node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty).Split(',');
                                if(node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty) != "0")
                                {
                                    //For each of the users event IDs calling the getSubscribedEvents function to the event details from event XML
                                    foreach (String id in eventids)
                                        table += getSubscribedEvents(id, table);
                                }
                                else
                                {
                                    Label1.Text = "There are no events to display";
                                    flag = true;
                                }
                                break;
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("File doesn't exist \n");
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
            if (!flag)
            {
                Label1.Text = table + "</table>"+ "<style>table, th, td {    border: 1px solid black;}</ style > ";
            }
            
        }

        //Getting the event details from event XML  for a particular user
        string getSubscribedEvents(string id,string table)
        {
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            string usermail = CookieVar["UserSessionCookie"], App_Data_Path;
            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"events.xml");
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList xnList = xmldoc.SelectNodes("/eventDetails");
                    foreach (XmlNode xn in xnList)
                    {
                        XmlNodeList CNodes = xn.SelectNodes("event");
                        foreach (XmlNode node in CNodes)
                        {
                            if (id == node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty))
                            {
                                string d0 = node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty);
                                string d1 = node["data"].InnerText.Trim().Replace("\r\n", string.Empty);
                                string d2 = node["date"].InnerText.Trim().Replace("\r\n", string.Empty);
                                string d3 = node["time"].InnerText.Trim().Replace("\r\n", string.Empty);
                                table = "<tr><td>" + d0 + "</td><td>" + d1 + "</td><td>" + d2 + "</td><td>" + d3 + "</td></tr>";
                                    break;
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("File doesn't exist \n");
                   
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
            return table;
        }
    }
}