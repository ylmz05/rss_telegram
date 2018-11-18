using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class AddGroupInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public AddGroupInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }
        public void Execute()
        {
            _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.Message.Chat.Id, "simply, invite me to your group.").GetAwaiter();
        }
    }
}
