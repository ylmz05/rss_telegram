using RabbitMQ.Client;
using Rss.RabbitMQ.Server;
using System.Text;

namespace Rss.RabbitMQ.Client.Producer
{
    public class ProduceMessage : IProduceMessage
    {
        private readonly IMessageQueueServer _messageQueueServer;

        public ProduceMessage()
        {
            _messageQueueServer = new MessageQueueServer();
        }

        public void ProduceMessageString(string queueName, string message)
        {
            using (var connection = _messageQueueServer.CreateServer())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
                }
            }
        }

        public void ProduceMessageByte(string queue, byte[] message)
        {
            using (var connection = _messageQueueServer.CreateServer())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    channel.BasicPublish("", queue, null, message);
                }
            }
        }
    }
}
