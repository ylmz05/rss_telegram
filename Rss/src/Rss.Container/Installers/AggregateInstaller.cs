using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rss.Container.Helpers;

namespace Rss.Container.Installers
{
	public class AggregateInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
            ContainerRegisterHelper.RegisterAggregates(container);
		}
	}
}
