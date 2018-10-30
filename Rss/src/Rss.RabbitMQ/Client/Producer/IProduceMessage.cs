namespace Rss.RabbitMQ.Client.Producer
{
    public interface IProduceMessage
    {
        void ProduceMessageString(string queueName, string message);
        void ProduceMessageByte(string queue, byte[] message);
    }
}