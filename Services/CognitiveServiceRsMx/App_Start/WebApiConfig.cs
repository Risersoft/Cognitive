using risersoft.shared.bot;
using risersoft.shared.web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CognitiveServiceRsMx
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerSelector), new MyHttpControllerSelector(config));

            // Web API configuration and services
            UnityConfig.RegisterComponents(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));

        }

    }
}
