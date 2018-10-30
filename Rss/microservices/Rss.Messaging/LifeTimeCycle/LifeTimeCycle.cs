using Rss.CDO.RabbitMQ.Response;
using Rss.Messaging.AppComponents;
using Rss.Messaging.Exceptions;
using Rss.RabbitMQ.Client.Consumer;
using System;
using System.Threading;
using Telegram.Bot;

namespace Rss.Messaging.LifeTimeCycle
{
    public class LifeTimeCycle
    {
        private static readonly TelegramBotClient _telegramBotClient = new TelegramBotClient(Environment.GetEnvironmentVariable("botToken"));
        public static void Create(MessagingPlatform messagingPlatform)
        {
            try
            {
                IConsumeMessage consumeMessage = new ConsumeMessage();
                while (true)
                {
                    Thread.Sleep(1000);
                    string consumedMessage = consumeMessage.ConsumeMessages(Environment.GetEnvironmentVariable("queueName"));
                    if (!string.IsNullOrEmpty(consumedMessage))
                    {
                        RabbitMQResponse response = RabbitMQResponse.Create(consumedMessage);
                        _telegramBotClient.SendTextMessageAsync(response.Payload.ChatId, response.Payload.Message).GetAwaiter().GetResult();
                    }
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                new ExceptionResponsibility(ex, messagingPlatform);
                Console.WriteLine(ex.ToString());
                Create(messagingPlatform);
            }
        }
    }
}
