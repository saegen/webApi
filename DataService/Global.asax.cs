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
    using NLog;
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
            TestDbConnection();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error($"Ett ohanterat fel av typen: {e.ExceptionObject.GetType()} uppstod! i {e.ExceptionObject}");
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
            NLog.LogManager.Shutdown();
        }
        private void TestDbConnection()
        {
            log = log ?? LogManager.GetCurrentClassLogger();
            string EFconString = ConfigurationManager.ConnectionStrings["rebtelEntities"].ToString();
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (string.IsNullOrEmpty(EFconString))
            {
                log.Error("Connectionstring is empty/missing!");
                throw new Exception("Connectionstring is empty/missing!");
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
            catch (Exception ex)
            {
                log.Error($"{userName} can't open connection to database");
                throw new Exception("Can't open/access database");
                //throw new FaultException("Can't open connection to database");
            }
        }
    }
}