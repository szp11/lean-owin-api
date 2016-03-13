using System;
using System.Configuration;
using LeanOwinApi.Services;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Options;

namespace LeanOwinApi
{
    public sealed class Service
    {
        private readonly IConfigurationService _configurationService;
        public string ServiceDescription => _configurationService.GetString("Description");
        public string DisplayName => _configurationService.GetString("DisplayName");
        public string ServiceName => _configurationService.GetString("Name");
        public string BaseAddress => _configurationService.GetString("BaseAddress");

        private IDisposable _server;

        public Service(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        
        public static void ServiceConfiguration(HostConfigurator x)
        {
            var service = UnityConfig.DependencyResolver.GetService(typeof(Service)) as Service;

            x.UseLinuxIfAvailable();

            x.Service<Service>(svc =>
            {
                svc.ConstructUsing(s => service);
                svc.WhenStarted(s => s.OnStart());
                svc.WhenPaused(s => s.OnPause());
                svc.WhenContinued(s => s.OnContinue());
                svc.WhenStopped(s => s.OnStop());
                
            });

            x.RunAsLocalSystem();

            x.SetDescription(service.ServiceDescription);
            x.SetDisplayName(service.DisplayName);
            x.SetServiceName(service.ServiceName);

            x.StartAutomatically();
        }

        public void OnStart()
        {
            var startOptions = new StartOptions(BaseAddress);
            _server = WebApp.Start<Startup>(startOptions);
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
