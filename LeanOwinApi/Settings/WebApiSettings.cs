using System.Collections.Generic;

namespace LeanOwinApi.Settings
{
    public sealed class WebApiSettings
    {
        public string HelloWorldMessage { get; set; }
        public IEnumerable<string> CorsAllowedOrigins { get; set; }
    }
}
