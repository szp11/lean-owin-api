using Microsoft.Owin.Cors;
using Owin;
using LeanOwinApi.Config;

namespace LeanOwinApi
{
    public sealed class OwinStartup
    {
        static void Main()
        {
            // Unity config happens before OWIN starts.
            UnityConfig.Configure();
            ServiceConfig.Configure();
        }

        public void Configuration(IAppBuilder app)
        {
            // Get web api configuration.
            WebApiConfig.Configure(app);
            // File server configuration.
            FileServerConfig.Configure(app);
            
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
