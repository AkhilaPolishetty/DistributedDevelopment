using System;

using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.Security;

namespace EventManagement.UserRoles.StaffPages
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            String userRole = Request.QueryString["PageName"];
            if (Session["SessionVar"] == null)
            {
                // Stop Caching in IE
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

                // Stop Caching in Firefox
                Response.Cache.SetNoStore();
                Response.Redirect("~/Default.aspx");
            }
            HttpCookie CookieVar = Request.Cookies["UserSessionCookie"];
            if ((CookieVar == null) || (CookieVar["UserSessionCookie"] == "") && userRole.Contains("AdminFeed"))
            {
                
                ResultLoginAdmin.Text = "<h2>" + "Welcome Admin!";
            }
            else
            {
                // Stop Caching in IE
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

                // Stop Caching in Firefox
                Response.Cache.SetNoStore();
                ResultLoginAdmin.Text = "<h2>" + "Welcome " + "Admin" + " " + CookieVar["UserSessionCookie"] + "!";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Details empty')", true);
                return;
            }

            //getEventID from last event in xml and increment it
            string eventID = "12", App_Data_Path;
            int eventIDCurrent = 12;
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
                            eventID = node["eventID"].InnerText.Trim().Replace("\r\n", string.Empty);
                    }
                    eventIDCurrent = Convert.ToInt32(eventID) + 1;
                }
                else
                {
                    Console.WriteLine("File doesn't exist \n");
                }
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
            App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
            XDocument doc = XDocument.Load(Path.Combine(App_Data_Path, @"events.xml"));
            XElement root = new XElement("event");
            root.Add(new XElement("eventID", eventIDCurrent));
            root.Add(new XElement("data", TextBox1.Text));
            root.Add(new XElement("date", Calendar1.SelectedDate.ToShortDateString()));
            root.Add(new XElement("time", DateTime.Now.ToString("h:mm:ss tt")));
            doc.Element("eventDetails").Add(root);
            doc.Save(Path.Combine(App_Data_Path, @"events.xml"));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Event Added Successfully')", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "")
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "RegisterSuccessful", "alert('Enter an event id');", true);
            string App_Data_Path;
            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"events.xml");
                XDocument doc = XDocument.Load(Path.Combine(App_Data_Path, @"events.xml"));
                bool found = false;
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList CNodes = xmldoc.GetElementsByTagName("event");
                    foreach (XmlNode node in CNodes)
                    {
                        XmlNodeList events = node.ChildNodes;
                        if (events.Item(0).InnerText.Equals(TextBox2.Text))
                        {
                            node.ParentNode.RemoveChild(node);
                            xmldoc.Save(Path.Combine(App_Data_Path, @"events.xml"));
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "RegisterSuccessful", "alert('Event deleted');", true);
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "RegisterSuccessful", "alert('No event id. Please enter proper number !');", true);

                }
                else
                {
                    Console.WriteLine("File doesn't exist \n");
                }
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text == "")
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "enterID", "alert('Enter an event id');", true);
            string App_Data_Path;
            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
                XDocument doc = XDocument.Load(Path.Combine(App_Data_Path, @"users.xml"));
                bool found = false;
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList CNodes = xmldoc.GetElementsByTagName("user");
                    foreach (XmlNode node in CNodes)
                    {
                        XmlNodeList events = node.ChildNodes;
                        if (events.Item(3).InnerText.Equals(TextBox3.Text))
                        {
                            if (events.Item(1).InnerText == "false")
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "clientdisabled", "alert('Member Already Disabled');", true);
                            else
                            {
                                events.Item(1).InnerText = "false";
                                xmldoc.Save(Path.Combine(App_Data_Path, @"users.xml"));
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "clientdisabled", "alert('Member Disabled');", true);
                            }

                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "22", "alert('No user with given email. Please enter proper email !');", true);

                }
                else
                {
                    Console.WriteLine("File doesn't exist \n");
                }
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }



        protected void LogOut(object sender, EventArgs e)
        {
            if (Request.Cookies["UserSessionCookie"] != null)
            {
                HttpCookie CookieKill = new HttpCookie("UserSessionCookie");
                CookieKill.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(CookieKill);
            }
            Session["SessionVar"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            //// Stop Caching in IE
            //    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            //    // Stop Caching in Firefox
            //    Response.Cache.SetNoStore();
            Response.Redirect("~/Default.aspx");
        }

        protected void AddAnotherAdmin_Click(object sender, EventArgs e)
        {
            string userRole = "";
            if (Session["SessionVar"] != null)
            {
                String sessionStr = Session["SessionVar"].ToString();
                if (sessionStr.Equals("AdminRegister"))
                {
                    userRole = HttpUtility.UrlEncode("AdminRegister");
                    Response.Redirect("/Account/Register.aspx?PageName=" + userRole);
                }
                else
                {
                    userRole = HttpUtility.UrlEncode("AdminRegister");
                    Response.Redirect("/Account/Register.aspx?PageName=" + userRole);
                }
            }
            else
            {
                userRole = HttpUtility.UrlEncode("AdminRegister");
                Response.Redirect("/Account/Register.aspx?PageName=" + userRole);
            }

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (TextBox4.Text == "")
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "enterID", "alert('Enter an event id');", true);
            string App_Data_Path;
            try
            {
                App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");
                string xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
                XDocument doc = XDocument.Load(Path.Combine(App_Data_Path, @"users.xml"));
                bool found = false;
                if (File.Exists(xmlfile_path))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);
                    XmlNodeList CNodes = xmldoc.GetElementsByTagName("user");
                    foreach (XmlNode node in CNodes)
                    {
                        XmlNodeList events = node.ChildNodes;
                        if (events.Item(3).InnerText.Equals(TextBox4.Text))
                        {
                            if (events.Item(1).InnerText == "true")
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "clientdisabled", "alert('Member Already Enabled');", true);

                            }
                            else
                            {
                                events.Item(1).InnerText = "true";
                                xmldoc.Save(Path.Combine(App_Data_Path, @"users.xml"));
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "clientdisabled", "alert('Member Enabled');", true);
                            }

                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "22", "alert('No user with given email. Please enter proper email !');", true);

                }
                else
                {
                    Console.WriteLine("File doesn't exist \n");
                }
            }

            catch (XmlException e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        
    }

}