using Rss.Container.App;
using Rss.Messaging.AppComponents;
using System;
using System.Threading;

namespace Rss.Messaging
{
    class Program
    {
        private static MessagingPlatform _messagingPlatform = new MessagingPlatform();

        static void Main(string[] args)
        {
            Thread.Sleep(60000);
            App app = new App(_messagingPlatform);
            app.StartContainer();

            LifeTimeCycle.LifeTimeCycle.Create(_messagingPlatform);
        }
    }
}
