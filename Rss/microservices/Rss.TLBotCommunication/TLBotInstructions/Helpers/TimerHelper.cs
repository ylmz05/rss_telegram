using System.Threading.Tasks;
using Telegram.Bot;

namespace Rss.TLBotCommunication.TLBotInstructions.Helpers
{
    class TimerHelper
    {
        private const int USER_DELAY = 100000;
        public static bool IsDataReceived { get; set; }

        public static void WaitUserValidation(ITelegramBotClient telegramBotClient, long chatId, long userId, string command)
        {
            Task.Run(() =>
            {
                Task.Delay(USER_DELAY).Wait();
                if (!IsDataReceived)
                {
                    telegramBotClient.SendTextMessageAsync(chatId, $"please call {command} again. time is out.");
                    SessionHelper.GetSession(userId).CanAddChannel = true;
                }
            });
        }
    }
}
