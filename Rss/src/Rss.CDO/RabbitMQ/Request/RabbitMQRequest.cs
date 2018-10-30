using Newtonsoft.Json;
using Rss.CDO.RabbitMQ.Model;
using Rss.RabbitMQ.Client.Producer;
using System;

namespace Rss.CDO.RabbitMQ.Request
{
    public class RabbitMQRequest
    {
        private RabbitMQRequest(long chatId, string message)
        {
            IProduceMessage produceMessage = new ProduceMessage();
            produceMessage.ProduceMessageString(Environment.GetEnvironmentVariable("queueName"), JsonConvert.SerializeObject(new Payload() { ChatId = chatId, Message = message }));
        }

        public static void Create(long chatId, string message)
        {
            new RabbitMQRequest(chatId, message);
        }
    }
}
