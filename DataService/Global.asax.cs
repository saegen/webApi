using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DataService
{
    public class Global : System.Web.HttpApplication
    {
        private Logger log; 
        protected void Application_Start(object sender, EventArgs e)
        {
            //Registrera GlobalExceptionHandler this.event += new Except nåt nåt
            //app domain exceptionhandler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LogManager.LoadConfiguration("nlog.config");
            log = LogManager.GetCurrentClassLogger();
            log.Info("Application_Start: Dataservice startar på IIS");
            TestDbConnection();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Tools.LogException(e.ExceptionObject);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
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
            log.Info("Application_End: Dataservice stängs ner på IIS");
        }
        private void TestDbConnection()
        {
            log = log ?? LogManager.GetCurrentClassLogger();
            string EFconString = ConfigurationManager.ConnectionStrings["rebtelEntities"].ToString();
            if (string.IsNullOrEmpty(EFconString))
            {
                log.Error("Connetionstring is empty");
                throw new Exception("Connetionstring is empty/missing!");
            }
            EntityConnectionStringBuilder c = new EntityConnectionStringBuilder(EFconString);
            
            
            log.Info("Connectar: " + c.ProviderConnectionString);
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(c.ProviderConnectionString);
                con.Open();
                con.Close();
                log.Debug("Database connection is OK");
            }
            catch (Exception)
            {
                log.Error("Can't open connection to database");
                throw new Exception("Can't open/access database");
                //throw new FaultException("Can't open connection to database");
            }
        }
    }
}