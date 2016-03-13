using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LeanOwinApi
{
    internal sealed class WebApiConfig
    {
        public static HttpConfiguration Configure()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            ConfigureJsonSerializerSettings(config);

            config.DependencyResolver = UnityConfig.DependencyResolver;
            return config;
        }

        private static void ConfigureJsonSerializerSettings(HttpConfiguration config)
        {
            var serializerSettings = config.Formatters.JsonFormatter.SerializerSettings ?? new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ" });
            serializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = serializerSettings;
        }
    }
}
