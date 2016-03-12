using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace LeanOwinApi
{
    internal sealed class UnityConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            UnityContainer.RegisterType(typeof(Service));

            // Type registration...
            // UnityContainer.RegisterType<ILogger, Logger>();
            // UnityContainer.RegisterType<IFakeService, FakeService>(new InjectionConstructor(false));

            config.DependencyResolver = new UnityDependencyResolver(UnityContainer);
        }

        #region Singleton implementation
        private static readonly Lazy<IUnityContainer> _unityContainer
                = new Lazy<IUnityContainer>(() =>
                {
                    var container = new UnityContainer();
                    return container;
                });

        private static IUnityContainer UnityContainer => _unityContainer.Value;
        #endregion

        #region Unity Dependency Resolver Implementation
        internal sealed class UnityDependencyResolver : IDependencyResolver
        {
            private readonly IUnityContainer _container;

            /// <summary>
            /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class for a _container.
            /// </summary>
            /// <param name="container">The <see cref="IUnityContainer"/> to wrap with the <see cref="IDependencyResolver"/>
            /// interface implementation.</param>
            public UnityDependencyResolver(IUnityContainer container)
            {
                if (container == null)
                    throw new ArgumentNullException(nameof(container));

                _container = container;
            }

            /// <summary>
            /// Reuses the same scope to resolve all the instances.
            /// </summary>
            /// <returns>The shared dependency scope.</returns>
            public IDependencyScope BeginScope()
            {
                var childContainer = _container.CreateChildContainer();
                return new UnityDependencyResolver(childContainer);
            }

            /// <summary>
            /// Disposes the wrapped <see cref="IUnityContainer"/>.
            /// </summary>
            public void Dispose()
            {
                _container.Dispose();
            }

            /// <summary>
            /// Resolves an instance of the default requested type from the _container.
            /// </summary>
            /// <param name="serviceType">The <see cref="Type"/> of the object to get from the _container.</param>
            /// <returns>The requested object.</returns>
            public object GetService(Type serviceType)
            {
                if (typeof(ApiController).IsAssignableFrom(serviceType))
                {
                    return _container.Resolve(serviceType);
                }
                try
                {
                    return _container.Resolve(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return null;
                }
            }

            /// <summary>
            /// Resolves multiply registered services.
            /// </summary>
            /// <param name="serviceType">The type of the requested services.</param>
            /// <returns>The requested services.</returns>
            public IEnumerable<object> GetServices(Type serviceType)
            {
                try
                {
                    return _container.ResolveAll(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return new List<object>();
                }
            }
        }
        #endregion
    }
}
