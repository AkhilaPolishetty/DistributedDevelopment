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
            //Taking the user role to be able to select the specific user xml file
            String userRole = Request.QueryString["PageName"];
            RegisterHyperLink.NavigateUrl = "/Account/Register.aspx?PageName=" + "register";

            if (userRole != null)
            {
                if (userRole.Contains("MemberLogin"))
                {
                    //Setting the lable on aspx page for the particular user
                    LoginPageId.InnerText = "Log in to access Member Page";
                }
                else
                {
                    //Session["AdminDetails1"] = Application["AdminDetailsUser"].ToString();
                    //Session["AdminDetails2"] = Application["AdminDetailsUserPassword"].ToString();
                    ProviderLabel.Text = "  <p><b> Provider:</b>Akhila <br/> <b>Functionality Name: </b> Global asax <br /> <b> Functionality Description: </b> This feature is used in the application to print the details of the TA admin login only on the admin page not on the member page <br /></p>";
                    AdminUserName.Text = "<h4>"+ "AdminName:" + Application["AdminDetailsUser"].ToString();
                    AdminPassword.Text = "<h4> AdminPassword:" + Application["AdminDetailsUserPassword"].ToString();
                    LoginPageId.InnerText = "Log in to access Admin Page";
                }

            }
            else
            {
                //If the user role is null redirecting to the default page
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(userEmail.Text) && !String.IsNullOrWhiteSpace(Password.Text)) 
            {
                //Using cookie to remeber the user
                HttpCookie CookieVar = new HttpCookie("UserSessionCookie");
                string sessionResult = accessingUser(userEmail.Text, Password.Text);
                String userRole = Request.QueryString["PageName"];

                if (userRole != null)
                {
                    if (userRole.Contains("MemberLogin"))
                    {
                        if (sessionResult.Equals("Login successful"))
                        {
                            //If the login is successful storing the useremai
                            CookieVar["UserSessionCookie"] = userEmail.Text;

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
                            CookieVar["UserSessionCookie"] = userEmail.Text;
                            CookieVar.Expires = DateTime.Now.AddMonths(1);
                            Response.Cookies.Add(CookieVar);
                            FormsAuthentication.SetAuthCookie(sessionResult, rememberme.Checked);
                            FormsAuthentication.RedirectFromLoginPage(sessionResult, rememberme.Checked);
                            Response.Redirect("/UserRoles/StaffPages/StaffPages.aspx?PageName=" + "AdminFeed");
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


        private string accessingUser(string userEmail, string Password)
        {
            string xmlfile_path="";
            string result = null;
            String userRole = Request.QueryString["PageName"];
            string App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");

            //Checking the user role to select the correct xml file
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
                        //Getting the user details
                        string useremail_fromxml = node["email"].InnerText.Trim().Replace("\r\n", string.Empty);
                        string password_fromxml = node["password"].InnerText.Trim().Replace("\r\n", string.Empty);
                        string enabled_fromxml = node["enabled"].InnerText.Trim().Replace("\r\n", string.Empty);

                        //Decrypting the encrypted password in the xml using DLL class
                        string DecryptedPassword = Class1.Decrypt(password_fromxml);

                        //Checking if the user logging in is present in the users.xml file
                        if (useremail_fromxml.Equals(userEmail))
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