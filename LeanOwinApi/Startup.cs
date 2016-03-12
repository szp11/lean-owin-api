using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



            app.UseWebApi(httpConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
