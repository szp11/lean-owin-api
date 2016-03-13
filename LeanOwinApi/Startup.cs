using Microsoft.Owin.Cors;
using Owin;
using Topshelf;

namespace LeanOwinApi
{
    public sealed class Startup
    {
        static void Main()
        {
            // Unity config happens before OWIN starts.
            UnityConfig.Configure();
            HostFactory.Run(Service.ServiceConfiguration);

        }
        
        public void Configuration(IAppBuilder app)
        {
            // Get web api configuration.
            var webApiConfiguration = WebApiConfig.Configure();

            app.UseWebApi(webApiConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
