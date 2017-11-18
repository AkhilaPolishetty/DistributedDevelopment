using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                ResultLogin.Text = "Welcome " + CookieVar["UserSessionCookie"] + "!";
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
            Response.Redirect("~/Default.aspx");
        }
    }
}