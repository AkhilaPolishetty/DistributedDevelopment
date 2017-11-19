using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace EventManagement
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Storing the details of the Admin
            Application["AdminDetails"] = null;
        }
        void Application_BeginRequest(Object Sender, EventArgs e)
        {
            Application.Lock();
            Application["AdminDetailsUser"] = "TA@asu.edu";
            Application["AdminDetailsUserPassword"] = "Cse445ta!";
            Application.UnLock();
         }

    void Session_Start(object sender, EventArgs e)
        {
          
        }
        void Session_End(object sender, EventArgs e)
        { 
         
        }

        void Applicartion_End()
        {
            Response.Write("<hr/> This page was last accessed at" + DateTime.Now.ToString());
        }
    }
}