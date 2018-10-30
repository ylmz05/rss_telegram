using Rss.Telegram.ServiceInterfaces;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Rss.Telegram.Services
{
    public class TLSendMessageService : ITLSendMessageService
    {
        private readonly ITLBotService _tLBotService;

        public TLSendMessageService(ITLBotService tLBotService)
        {
            _tLBotService = tLBotService;
        }

        public void SendMessage(string channel, string message)
        {
            ITelegramBotClient telegramBotClient = _tLBotService.CreateBot();

            Message messageResult = telegramBotClient.SendTextMessageAsync(string.Concat("-100", channel), HttpUtility.HtmlDecode(message)).GetAwaiter().GetResult();
        }
    }
}
