using System;
using LeanOwinApi.Settings;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using Topshelf;

namespace LeanOwinApi.Config
{
    internal sealed class ServiceConfig
    {
        [Dependency]
        public ServiceSettings ServiceSettings { get; set; }
        [Dependency]
        public WebApiSettings WebApiSettings { get; set; }
        
        private IDisposable _server;

        public static void Configure()
        {
            HostFactory.Run(x =>
            {
                var service = UnityConfig.DependencyResolver.GetService(typeof(ServiceConfig)) as ServiceConfig;

                x.UseLinuxIfAvailable();

                x.Service<ServiceConfig>(svc =>
                {
                    svc.ConstructUsing(s => service);
                    svc.WhenStarted(s => s.OnStart());
                    svc.WhenPaused(s => s.OnPause());
                    svc.WhenContinued(s => s.OnContinue());
                    svc.WhenStopped(s => s.OnStop());

                });

                x.RunAsLocalSystem();

                x.SetDescription(service.ServiceSettings.Description);
                x.SetDisplayName(service.ServiceSettings.DisplayName);
                x.SetServiceName(service.ServiceSettings.Name);

                x.StartAutomatically();
            });
        }

        public void OnStart()
        {
            var startOptions = new StartOptions(WebApiSettings.BaseUrl);
            _server = WebApp.Start<OwinStartup>(startOptions);
        }

        public void OnStop()
        {
            _server?.Dispose();
        }

        public void OnPause()
        {
            // not implemented
        }

        public void OnContinue()
        {
            // not implemented
        }
    }
}
