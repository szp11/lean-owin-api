using System;
using LeanOwinApi.Services;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace LeanOwinApi.Config
{
    internal sealed class ServiceConfig
    {
        private readonly IAppSettingsService _appSettingsService;
        public string ServiceDescription => _appSettingsService.GetString("Description");
        public string DisplayName => _appSettingsService.GetString("DisplayName");
        public string ServiceName => _appSettingsService.GetString("Name");
        public string BaseAddress => _appSettingsService.GetString("BaseAddress");

        private IDisposable _server;

        public ServiceConfig(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

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

                x.SetDescription(service.ServiceDescription);
                x.SetDisplayName(service.DisplayName);
                x.SetServiceName(service.ServiceName);

                x.StartAutomatically();
            });
        }

        public void OnStart()
        {
            var startOptions = new StartOptions(BaseAddress);
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
