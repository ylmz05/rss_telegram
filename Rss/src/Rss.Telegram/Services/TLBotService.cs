using Rss.Telegram.ServiceInterfaces;
using System;
using Telegram.Bot;

namespace Rss.Telegram.Services
{
    public class TLBotService : ITLBotService
    {
        private readonly string _token;

        public TLBotService(string token)
        {
            _token = token;
        }

        public ITelegramBotClient CreateBot()
        {
            return new TelegramBotClient(_token);
        }
    }
}
