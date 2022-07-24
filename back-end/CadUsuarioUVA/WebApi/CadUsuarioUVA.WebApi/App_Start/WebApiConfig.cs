using CadUsuarioUVA.IoC;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CadUsuarioUVA.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters
                .JsonFormatter
                .SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.DependencyResolver =
                new IoCResolverConfiguration(RegisterDependency.Register());
        }
    }
}