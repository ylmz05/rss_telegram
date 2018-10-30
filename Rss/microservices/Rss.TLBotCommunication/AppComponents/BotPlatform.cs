using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Rss.Container.AppInterfaces;
using Rss.Container.Installers;

namespace Rss.Messaging.AppComponents
{
    public class BotPlatform : IPlatformProvider
    {
        private static IWindsorContainer _windsorCompanents;

        public void Initialize()
        {
            IWindsorInstaller[] castleCompanents = new IWindsorInstaller[]
            {
                new InterceptorInstaller(),
                new ServiceInstaller(),
                new RepoInstaller(),
                new AggregateInstaller()
            };

            _windsorCompanents = new WindsorContainer().AddFacility<TypedFactoryFacility>();
            _windsorCompanents.Install(castleCompanents);
        }

        public T Resolve<T>()
        {
            return _windsorCompanents.Resolve<T>();
        }
    }
}
