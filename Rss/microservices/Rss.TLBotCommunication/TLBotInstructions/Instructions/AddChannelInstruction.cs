using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class AddChannelInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        public AddChannelInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
        }
        public void Execute()
        {
            SessionHelper.GetSession(_messageEventArgs.Message.From.Id).Code = RandomCodeHelper.Produce(50);
            SessionHelper.GetSession(_messageEventArgs.Message.From.Id).CanAddChannel = false;
            _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, $"send below code as message from channel that contain me. code : {SessionHelper.GetSession(_messageEventArgs.Message.From.Id).Code}").GetAwaiter();
            TimerHelper.WaitUserValidation(_telegramBotClient, _messageEventArgs.Message.From.Id, _messageEventArgs.Message.From.Id, "/addchannel");
        }
    }
}
