using Rss.Container.AppInterfaces;
using Rss.Persistence.MySQL.Dapper;

namespace Rss.Container.App
{
    public class App
    {
        private readonly IPlatformProvider _platformProvider;

        public App(IPlatformProvider platformProvider)
        {
            _platformProvider = platformProvider;
        }

        public void StartContainer()
        {
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { typeof(BotEntityMapper).Assembly });
            DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.MySqlDialect();

            _platformProvider.Initialize();
        }
    }
}
