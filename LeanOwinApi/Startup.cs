using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using Topshelf;

namespace LeanOwinApi
{
    public sealed class Startup
    {
        static void Main()
        {
            HostFactory.Run(Service.ServiceConfiguration);
        }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            WebApiConfig.Configure(httpConfiguration);
            UnityConfig.Configure(httpConfiguration);

            app.UseWebApi(httpConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
