using System;
using Microsoft.Owin.Hosting;
using Topshelf;
using Topshelf.HostConfigurators;

namespace LeanOwinApi
{
    public sealed class Service
    {
        public static readonly string BaseAddress = "http://localhost:1337";
        public static readonly string DisplayName = "Lean OWIN API Service";
        public static readonly string Name = "LeanOwinApi";
        public static readonly string Description = "Self-hosted OWIN API.";
        private IDisposable _server;

        public static void ServiceConfiguration(HostConfigurator x)
        {
            x.UseLinuxIfAvailable();

            x.Service<Service>(svc =>
            {
                svc.ConstructUsing(name => new Service());
                svc.WhenStarted(s => s.OnStart());
                svc.WhenPaused(s => s.OnPause());
                svc.WhenContinued(s => s.OnContinue());
                svc.WhenStopped(s => s.OnStop());
            });

            x.RunAsLocalSystem();

            x.SetDescription(Description);
            x.SetDisplayName(DisplayName);
            x.SetServiceName(Name);

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
