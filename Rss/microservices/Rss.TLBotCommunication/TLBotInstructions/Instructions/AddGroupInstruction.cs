using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class AddGroupInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        public AddGroupInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
        }
        public void Execute()
        {
            _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "simply, invite me to your group.").GetAwaiter();
        }
    }
}
