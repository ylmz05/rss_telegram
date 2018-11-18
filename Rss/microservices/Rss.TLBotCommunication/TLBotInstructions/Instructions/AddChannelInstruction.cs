using Rss.CDO.Enums.TLBot;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class AddChannelInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public AddChannelInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id);
            session.Code = RandomCodeHelper.Produce(50);
            session.InstructionId = NextInstruction.WaitingCode;
            _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.Message.Chat.Id, 
                "firstly, you have to add me to your channel as administrator within only post message permission. " +
                "after that forward the code as message to channel that contains me.").GetAwaiter();
            _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.Message.Chat.Id, session.Code).GetAwaiter();
            TimerHelper.WaitUserValidation(_telegramBotClient, _callbackQueryEventArgs.CallbackQuery.From.Id, _callbackQueryEventArgs.CallbackQuery.From.Id, "/addchannel");
        }
    }
}
