using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rss.Container.Helpers;

namespace Rss.Container.Installers
{
    public class RepoInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
            ContainerRegisterHelper.RegisterRepos(container);

            //container.Register(Component.For(typeof(IUnitOfWork)).
            //	ImplementedBy(typeof(MySQLUnitOfWork)).
            //	LifeStyle.Transient);
        }
    }
}
