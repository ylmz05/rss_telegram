using Rss.CDO.Enums.TLBot;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Rss.TLBotCommunication.TLBotInstructions.Helpers
{
    class TimerHelper
    {
        private const int USER_DELAY = 60000;
        private static bool IsDataReceived { get; set; }

        public static void WaitUserValidation(ITelegramBotClient telegramBotClient, long chatId, long userId, string command)
        {
            Task.Run(() =>
            {
                Task.Delay(USER_DELAY).Wait();
                if (!IsDataReceived)
                {
                    telegramBotClient.SendTextMessageAsync(chatId, $"please call {command} again. you did not use the code to add channel [TIMEOUT].");
                    SessionHelper.GetSession(userId).InstructionId = NextInstruction.CodeNotValidatedTimeOut;
                }
            });
        }
        public static void DataReceived()
        {
            IsDataReceived = true;
        }
    }
}
