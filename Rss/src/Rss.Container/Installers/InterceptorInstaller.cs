using Castle.DynamicProxy;
using Ioc.Interceptors;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Rss.Container.Installers
{
	public class InterceptorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IInterceptor>()
				.ImplementedBy<RepoInterceptor>()
				.LifeStyle.Transient);

            container.Register(Component.For<IInterceptor>()
				.ImplementedBy<ServiceInterceptor>()
				.LifeStyle.Transient);
		}
	}
}
