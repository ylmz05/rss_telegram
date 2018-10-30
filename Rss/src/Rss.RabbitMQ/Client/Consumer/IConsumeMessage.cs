namespace Rss.RabbitMQ.Client.Consumer
{
    public interface IConsumeMessage
    {
        string ConsumeMessages(string queueName);
    }
}