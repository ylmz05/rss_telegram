using Telegram.Bot;

namespace Rss.Telegram.ServiceInterfaces
{
    public interface ITLBotService
    {
        ITelegramBotClient CreateBot();
    }
}
