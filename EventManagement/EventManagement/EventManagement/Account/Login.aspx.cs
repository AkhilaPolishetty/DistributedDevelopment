using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using EventManagement.Models;
using System.IO;
using System.Xml;
using System.Web.Security;
using System.Web.UI.WebControls;
using EncryptionDecryption;

namespace EventManagement.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String userRole = Request.QueryString["PageName"];
            RegisterHyperLink.NavigateUrl = "/Account/Register.aspx?PageName=" + "register";

            if (userRole != null)
            {
                if (userRole.Contains("MemberLogin"))
                {
                    LoginPageId.InnerText = "Log in to access Member Page";
                }
                else
                {
                    //Session["AdminDetails1"] = Application["AdminDetailsUser"].ToString();
                    //Session["AdminDetails2"] = Application["AdminDetailsUserPassword"].ToString();
                    AdminUserName.Text = "AdminName: "+Application["AdminDetailsUser"].ToString();
                    AdminPassword.Text = "AdminPassword: " +Application["AdminDetailsUserPassword"].ToString();
                    LoginPageId.InnerText = "Log in to access Admin Page";
                }

            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(UserName.Text) && !String.IsNullOrWhiteSpace(Password.Text)) 
            {
                HttpCookie CookieVar = new HttpCookie("UserSessionCookie");
                string sessionResult = accessingUser(UserName.Text, Password.Text);
                String userRole = Request.QueryString["PageName"];

                if (userRole != null)
                {
                    if (userRole.Contains("MemberLogin"))
                    {
                        if (sessionResult.Equals("Login successful"))
                        {
                            CookieVar["UserSessionCookie"] = UserName.Text;
                            CookieVar.Expires = DateTime.Now.AddMonths(6);
                            Response.Cookies.Add(CookieVar);
                            FormsAuthentication.SetAuthCookie(sessionResult, rememberme.Checked);
                            FormsAuthentication.RedirectFromLoginPage(sessionResult, rememberme.Checked);
                            Response.Redirect("/UserRoles/UserPages/UserPages.aspx?PageName=" + "UserFeed");
                        }
                        else
                        {
                            ResultLogin.Text = sessionResult;
                        }
                    }
                    else
                    {
                        if (sessionResult.Equals("Login successful"))
                        {
                            CookieVar["UserSessionCookie"] = UserName.Text;
                            CookieVar.Expires = DateTime.Now.AddMonths(6);
                            Response.Cookies.Add(CookieVar);
                            FormsAuthentication.SetAuthCookie(sessionResult, rememberme.Checked);
                            FormsAuthentication.RedirectFromLoginPage(sessionResult, rememberme.Checked);
                            Response.Redirect("/UserRoles/StaffPages/Admin.aspx?PageName=" + "AdminFeed");
                        }
                        else
                        {
                            ResultLogin.Text = sessionResult;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }

               
            }
        }


        private string accessingUser(string UserName, string Password)
        {
            string xmlfile_path="";
            string result = null;
            String userRole = Request.QueryString["PageName"];
            string App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");

            if (userRole.Contains("MemberLogin"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "MemberLogin");
                xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
            }
            else if (userRole.Contains("AdminLogin"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminLogin");
                xmlfile_path = Path.Combine(App_Data_Path, @"staff.xml");
            }
            else
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminLogin");
                xmlfile_path = Path.Combine(App_Data_Path, @"staff.xml");
            }

            if (File.Exists(xmlfile_path))
            {
                //Loading the xml document which is used as database 
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlfile_path);

                XmlNodeList OuterList = xmldoc.SelectNodes("/UserDetails");
                foreach (XmlNode xn in OuterList)
                {
                    XmlNodeList UserList = xn.SelectNodes("user");
                    foreach (XmlNode node in UserList)
                    {
                        string username_fromxml = node["name"].InnerText.Trim().Replace("\r\n", string.Empty);
                        string password_fromxml = node["password"].InnerText.Trim().Replace("\r\n", string.Empty);
                        string enabled_fromxml = node["enabled"].InnerText.Trim().Replace("\r\n", string.Empty);
                        string DecryptedPassword = Class1.Decrypt(password_fromxml);

                        if (username_fromxml.Equals(UserName))
                        {
                            if (DecryptedPassword.Equals(Password))
                            {
                                if (enabled_fromxml.Equals("true"))
                                {
                                    result = "Login successful";
                                    break;
                                }
                                else
                                {
                                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "UserDeleted", "alert('Admin has disabled your account');", true);
                                    //Response.Write("Admin has  disabled your account");
                                }
                                
                            }
                            else
                            {
                                result = "Password does not match with username";
                                break;
                            }
                        }
                        else
                        {
                            result = "First register and then try to login";
                        }
                    }

                }
            }
            return result;

        }
    }
}