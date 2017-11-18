using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventManagement
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoginUser_Click(object sender, EventArgs e)
        {
            string userRole = "";
            if (Session["SessionVar"] != null)
            {
                String sessionStr = Session["SessionVar"].ToString();
                if (sessionStr.Equals("MemberLogin"))
                {
                   // userRole = HttpUtility.UrlEncode("MemberLogin");
                    Response.Redirect("UserRoles/UserPages/UserPages.aspx");
                }
                else
                {
                    Response.Write("Check your internet connection");
                    Response.Redirect("~/default.aspx");
                }
            }
            else
            {
                userRole = HttpUtility.UrlEncode("MemberLogin");
                Response.Redirect("/Account/Login.aspx?PageName=" + userRole);
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string userRole = "";
            if (Session["SessionVar"] != null)
            {
                String sessionStr = Session["SessionVar"].ToString();
                if (sessionStr.Equals("MemberRegister"))
                {
                    userRole = HttpUtility.UrlEncode("MemberRegister");
                    Response.Redirect("/Account/Register.aspx?PageName=" + userRole);
                }
                else
                {
                    Response.Write("Check your internet connection");
                    Response.Redirect("~/default.aspx");
                }
            }
            else
            {
                userRole = HttpUtility.UrlEncode("MemberRegister");
                Response.Redirect("/Account/Register.aspx?PageName=" + userRole);
            }
        }

        protected void LoginAdmin_Click(object sender, EventArgs e)
        {
            string userRole = "";
            if (Session["SessionVar"] != null)
            {
                String sessionStr = Session["SessionVar"].ToString();
                if (sessionStr.Equals("AdminLogin"))
                {
                    userRole = HttpUtility.UrlEncode("AdminLogin");
                    Response.Redirect("/Account/Login.aspx?PageName=" + userRole);
                }
                else
                {
                    Response.Write("Check your internet connection");
                    Response.Redirect("~/default.aspx");
                }
            }
            else
            {
                userRole = HttpUtility.UrlEncode("AdminLogin");
                Response.Redirect("/Account/Login.aspx?PageName=" + userRole);
            }
        }

        //protected void CreateAdmin_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/Account/Register.aspx?PageName=" + "AdminRegister");
        //}
    }
}