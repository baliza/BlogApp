using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using DependencyResolution;
using DependencyResolution.AppSStart;


namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        private static readonly ApiServiceConfiguration WebApiConfig = new ApiServiceConfiguration(GlobalConfiguration.Configuration);

        protected void Application_Start()
        {
            WebApiConfig.Configure();
        }
    }
}
