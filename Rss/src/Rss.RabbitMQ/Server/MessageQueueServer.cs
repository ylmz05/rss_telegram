using RabbitMQ.Client;
using System;

namespace Rss.RabbitMQ.Server
{
    public class MessageQueueServer : IMessageQueueServer
    {
        public IConnection CreateServer()
        {
            return new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("rabbimqHost") }.CreateConnection();
        }
    }
}
