using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;

namespace DataService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Registrera GlobalExceptionHandler this.event += new Except nåt nåt
            //app domain exceptionhandler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LogManager.LoadConfiguration("nlog.config");
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Tools.LogException(e.ExceptionObject);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            TestDbConnection();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void TestDbConnection()
        {
            string shorty = ConfigurationManager.ConnectionStrings["short"].ToString();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(shorty);
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                //log.Error("Can't open connection to database: {0}", dockerConIP);

                throw new Exception("Can't open/access database");
                //throw new FaultException("Can't open connection to database");
            }
        }
    }
}