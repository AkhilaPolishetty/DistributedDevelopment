using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using EventManagement.Models;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using EncryptionDecryption;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EventManagement.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String userRole = Request.QueryString["PageName"];
            //RegisterHyperLink.NavigateUrl = "/Account/Register.aspx?PageName=" + "register";

            if (userRole != null)
            {
                if (userRole.Contains("MemberRegister"))
                {
                    RegisterPageId.InnerText = "Register to access Member Page";
                }
                else
                {
                    RegisterPageId.InnerText = "Register to access Admin Page";
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //string[] attributes = details.Split('-');
            string xmlfile_path = "";
            string userRole = getUserRole();
            string name = UserName.Text.ToString();
            string email = Email.Text.ToString();
            string phone = Phone.Text.ToString();
            string password = Password.Text.ToString();
            string repeatpassword = ConfirmPassword.Text.ToString();
            //  string captcha = "";
            string outputErrorMsg = "";
            bool email_bool, phone_bool, password_bool, repeatpassword_bool, emailExists, registerSuccessful = false;
            //email validation
            email_bool = false;
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                email_bool = addr.Address == email;
                if (!email_bool)
                    outputErrorMsg += "Please enter a valid email id \n";

            }
            catch
            {
                outputErrorMsg += "Please enter a valid email id \n";
            }

            //check if mail id already exists in the database
            emailExists = false;
            int emailflag = 0;
            try
            {
                xmlfile_path = getSession();
                if (File.Exists(xmlfile_path)) {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlfile_path);

                    XmlNodeList xnList = xmldoc.SelectNodes("/UserDetails");
                    foreach (XmlNode xn in xnList) {
                        XmlNodeList CNodes = xn.SelectNodes("user");
                        foreach (XmlNode node in CNodes) {
                            string email_db = node["email"].InnerText.Trim().Replace("\r\n", string.Empty);
                            if (email_db == email) {
                                emailflag += 1;
                                //Console.WriteLine("email id already exists \n");
                                //return email_db;
                                outputErrorMsg = "Email id already exists \n";
                                break;
                            }
                        }
                    }
                } else {
                    Console.WriteLine("File doesn't exist \n");
                }
            } catch (XmlException em) {
                Console.WriteLine(em.Message);
                outputErrorMsg += em.Message;
            }
            if (emailflag > 0) {
                outputErrorMsg += "You have already been registered. Please Log in ! \n";
                emailExists = true;
            }

            //bool isNumeric = int.TryParse(phone, out var n);
            if (phone.Length == 10)
                phone_bool = true;
            else
                phone_bool = false;
            if (!phone_bool)
                outputErrorMsg += "Please enter a valid phone number \n";

            //password validation
            //The conditions are string must be between 8 and 15 characters long. 
            //string must contain at least one number. string must contain at least one uppercase letter. 
            //string must contain at least one lowercase letter.
            var input = password;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasLowerChar = new Regex(@"[a-z]+");

            var isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input) && hasLowerChar.IsMatch(input);
            Console.WriteLine(isValidated);
            password_bool = isValidated;
            if (!password_bool)
                outputErrorMsg += "Password not valid ! Please note string must be between 8 and 15 chars.\n " +
                    "Should contain atleast 1 number, 1 Uppercase letter, 1 lowercase letter \n";
            else
                Console.WriteLine("passwordd validated");

            //repeat password validation
            repeatpassword_bool = password == repeatpassword;
            if (!repeatpassword_bool)
                outputErrorMsg += "Passwords don't match ! \n";

            //captcha_bool = true; //temp change this
            if (email_bool && phone_bool && password_bool && repeatpassword_bool && !emailExists && IsCaptchaValid)
            {
                
                XDocument doc = XDocument.Load(xmlfile_path);
                XElement root = new XElement("user");
                root.Add(new XElement("name", name));
                root.Add(new XElement("eventID", 0));
                root.Add(new XElement("enabled", "true"));
                root.Add(new XElement("email", email));
                string EncryptPassword = Class1.Encrypt(password);
                root.Add(new XElement("password", EncryptPassword));
                root.Add(new XElement("phone", phone));
                doc.Element("UserDetails").Add(root);
                doc.Save(xmlfile_path);
                ResultRegister.Text = "Form validated and data has been saved";
                registerSuccessful = true;
            }
            //string output = "email_bool  " + email_bool + "  phone_bool " + phone_bool + " password_bool " + password_bool + " repeatpassword_bool " + repeatpassword_bool + " emailExists " + emailExists + " captcha_bool " + captcha_bool;

            if (registerSuccessful)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "RegisterSuccessful", "alert('Register Successful');", true);
                Response.Redirect("/Account/Login.aspx?PageName=" + userRole);
            }
            else
            {
                ResultRegister.Text = outputErrorMsg;
            }
        }
    

        public string getSession()
        {
            string xmlfile_path = "";
            //string result = null;
            String userRole = Request.QueryString["PageName"];
            string App_Data_Path = Path.Combine(HttpRuntime.AppDomainAppPath, @"XMLFiles");

            if (userRole.Contains("MemberRegister"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "MemberRegister");
                xmlfile_path = Path.Combine(App_Data_Path, @"users.xml");
            }
            else if (userRole.Contains("AdminRegister"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminRegister");
                xmlfile_path = Path.Combine(App_Data_Path, @"staff.xml");
            }
            else
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminRegister");
                xmlfile_path = Path.Combine(App_Data_Path, @"staff.xml");
            }

            return xmlfile_path;
        }

        public string getUserRole()
        {
            string result = "";
            String userRole = Request.QueryString["PageName"];
           
            if (userRole.Contains("MemberRegister"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "MemberRegister");
                result = "MemberLogin";
            }
            else if (userRole.Contains("AdminRegister"))
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminRegister");
                result = "AdminLogin";
            }
            else
            {
                Session.Remove("SessionVar");
                Session.Add("SessionVar", "AdminRegister");
                result = "AdminLogin";
            }

            return result;
        }
    }

    class ReCaptchaClass
    {
        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();

            string PrivateKey = "6LcoaTUUAAAAADN_-B23sD9CdHH1LFFA83wlXd6q";

            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}