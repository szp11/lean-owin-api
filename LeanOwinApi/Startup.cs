using System;
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
            UnityConfig.Configure();
            HostFactory.Run(Service.ServiceConfiguration);

        }

        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = WebApiConfig.Configure();
            app.UseWebApi(webApiConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
