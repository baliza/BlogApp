using DependencyResolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //GlobalConfiguration.Configuration.EnsureInitialized();
            //// Web API routes
            //config.MapHttpAttributeRoutes();
            //config.EnsureInitialized();
            var apiConfig = new ApiServiceConfiguration(config);
            apiConfig.Configure();
          
          //  config.Routes.MapHttpRoute(
          //      name: "DefaultApi",
          //      routeTemplate: "api/{controller}/{id}",
          //      defaults: new { controller = "Post", id = RouteParameter.Optional }
          //  );

          //  config.Routes.MapHttpRoute(
          //      name: "ActionApi",
          //      routeTemplate: "api/{controller}/{action}/{value}",
          //      defaults: new { value = RouteParameter.Optional }
          //  );
          //  config.Routes.MapHttpRoute(
          //    name: "FindApi",
          //    routeTemplate: "api/{controller}/{action}/{value1}/{value2}",
          //    defaults: new { value1 = RouteParameter.Optional, value2 = RouteParameter.Optional }
          //);
        }
    }
}
