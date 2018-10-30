using RabbitMQ.Client;
using Rss.RabbitMQ.Server;
using System.Text;

namespace Rss.RabbitMQ.Client.Consumer
{
    public class ConsumeMessage : IConsumeMessage
    {
        private readonly IMessageQueueServer _messageQueueServer;
        public ConsumeMessage()
        {
            _messageQueueServer = new MessageQueueServer();
        }

        public string ConsumeMessages(string queueName)
        {
            BasicGetResult data;
            using (var connection = _messageQueueServer.CreateServer())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    data = channel.BasicGet(queueName, true);
                }

            return data != null ? Encoding.UTF8.GetString(data.Body) : null;
        }
    }
}
