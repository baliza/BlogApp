using System;


using Core;
using System.Web.Http;
using System.Net.Http;
using DependencyResolution.DependencyResolution;
using Newtonsoft.Json;
using StructureMap;
using Newtonsoft.Json.Serialization;

namespace DependencyResolution
{

    public class ApiServiceConfiguration : IApiApplication
    {
        private readonly HttpConfiguration config;

        public ApiServiceConfiguration(HttpConfiguration configuration)
        {
            config = configuration;
        }

        public void Configure()
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            ConfigureResolver();
            ConfigureRoutes();
        }

        private void ConfigureResolver()
        {
           
            var container = IoC.Initialize();
            var resolver = new StructureMapDependencyResolver(container);
            config.DependencyResolver = resolver;
        }

        private void ConfigureRoutes()
        {
             
            config.MapHttpAttributeRoutes();
            
            config.EnsureInitialized();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           // config.Routes.MapHttpRoute(
           //     name: "DefaultApi",
           //     routeTemplate: "api/{controller}/{id}/{action}",
           //     defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional ,}
           //     );
           // config.Routes.MapHttpRoute(
           //    name: "ActionApi",
           //    routeTemplate: "api/{controller}/{action}/{value}",
           //    defaults: new { value = RouteParameter.Optional }
           //);


                        //config.Routes.MapHttpRoute(
                        //    name: "DefaultApi",
                        //    routeTemplate: "api/{controller}/{id}",
                        //    defaults: new { controller = "Post", id = RouteParameter.Optional }
                        //);

                        //config.Routes.MapHttpRoute(
                        //    name: "ActionApi",
                        //    routeTemplate: "api/{controller}/{action}/{value}",
                        //    defaults: new { value = RouteParameter.Optional }
                        //);
                        //config.Routes.MapHttpRoute(
                        //  name: "FindApi",
                        //  routeTemplate: "api/{controller}/{action}/{value1}/{value2}",
                        //  defaults: new { value1 = RouteParameter.Optional, value2 = RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

        }
    }
}
