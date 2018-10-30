using RabbitMQ.Client;

namespace Rss.RabbitMQ.Server
{
    public interface IMessageQueueServer
    {
        IConnection CreateServer();
    }
}