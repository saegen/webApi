using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Logger log = LogManager.GetLogger("jsonFile");
        protected void Application_Start()
        {
            LogManager.LoadConfiguration("nlog.config");
            log.Info("Application_Start: WebApi startar..");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log.Info("WebApi startad");

        }

        protected void Application_End(object sender, EventArgs e)
        {
            log.Info("Application_End: WebApi stängs ner på IIS");
            NLog.LogManager.Shutdown();
        }
        
    }
}
