using Rss.Container.App;
using Rss.Reader.AppComponents;
using System.Threading;

namespace Rss.Reader
{
    class Program
    {
        private static ReaderPlatform _readerPlatform = new ReaderPlatform();

        static void Main(string[] args)
        {
            Thread.Sleep(60000);
            App app = new App(_readerPlatform);
            app.StartContainer();

            LifeTimeCycle.LifeTimeCycle.Create(_readerPlatform);
        }
    }
}
