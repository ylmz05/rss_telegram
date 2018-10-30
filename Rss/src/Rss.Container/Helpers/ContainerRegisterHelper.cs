using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Ioc.Interceptors;
using Rss.Application.Aggregates.Classes.Queries;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Classes.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.Persistence.MySQL.Repos.Classes.Queries;
using Rss.Persistence.Repos.Interfaces.Queries;
using System;
using System.Linq;

namespace Rss.Container.Helpers
{
    public class ContainerRegisterHelper
    {
        public static void RegisterRepos(IWindsorContainer container)
        {
            var interfaces = typeof(IQueryBotRepo).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Persistence.Repos.Interfaces.Commands.") || x.FullName.StartsWith("Rss.Persistence.Repos.Interfaces.Queries.")).ToList();

            var classes = typeof(QueryBotRepo).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Persistence.MySQL.Repos.Classes.Commands.") || x.FullName.StartsWith("Rss.Persistence.MySQL.Repos.Classes.Queries.")).ToList();

            foreach (Type typeClass in classes)
            {
                Type typeInterface = interfaces.Find(x => x.IsAssignableFrom(typeClass));
                if (typeInterface == null)
                    continue;

                container.Register(Component.For(typeInterface)
                    .ImplementedBy(typeClass)
                    .Interceptors(new Type[] { typeof(RepoInterceptor) })
                    .DependsOn(Dependency.OnValue("connectionString", Environment.GetEnvironmentVariable("connectionString")))
                    .LifeStyle.Transient);
            }
        }

        public static void RegisterAggregates(IWindsorContainer container)
        {
            var interfaces = typeof(IQueryBotAggregate).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Application.Aggregates.Interfaces.Commands.") || x.FullName.StartsWith("Rss.Application.Aggregates.Interfaces.Queries.")).ToList();

            var classes = typeof(QueryBotAggregate).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Application.Aggregates.Classes.Commands.") || x.FullName.StartsWith("Rss.Application.Aggregates.Classes.Queries.")).ToList();

            foreach (Type typeClass in classes)
            {
                Type typeInterface = interfaces.Find(x => x.IsAssignableFrom(typeClass));
                if (typeInterface == null)
                    continue;

                container.Register(Component.For(typeInterface).
                    ImplementedBy(typeClass).
                    LifeStyle.Transient);
            }
        }

        public static void RegisterServices(IWindsorContainer container)
        {
            var interfaces = typeof(IQueryBotService).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Application.Services.Interfaces.Commands.") || x.FullName.StartsWith("Rss.Application.Services.Interfaces.Queries.")).ToList();

            var classes = typeof(QueryBotService).Assembly.GetExportedTypes()
                .Where(x => x.FullName.StartsWith("Rss.Application.Services.Classes.Commands.") || x.FullName.StartsWith("Rss.Application.Services.Classes.Queries.")).ToList();

            foreach (Type typeClass in classes)
            {
                Type typeInterface = interfaces.Find(x => x.IsAssignableFrom(typeClass));
                if (typeInterface == null)
                    continue;

                container.Register(Component.For(typeInterface).
                    ImplementedBy(typeClass)
                    .Interceptors(new Type[] { typeof(ServiceInterceptor) })
                    .LifeStyle.Transient);
            }
        }
    }
}
