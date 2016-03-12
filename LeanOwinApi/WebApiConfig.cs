using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LeanOwinApi
{
    internal sealed class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            RemoveXmlFormatter(config);

            ConfigureJsonSerializerSettings(config);
        }

        private static void ConfigureJsonSerializerSettings(HttpConfiguration config)
        {
            var serializerSettings = config.Formatters.JsonFormatter.SerializerSettings ?? new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ" });
            serializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = serializerSettings;
        }

        private static void RemoveXmlFormatter(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
