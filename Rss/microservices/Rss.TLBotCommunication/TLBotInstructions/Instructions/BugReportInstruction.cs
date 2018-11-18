using Rss.CDO.Enums.TLBot;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class BugReportInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        public BugReportInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_messageEventArgs.Message.From.Id);
            session.InstructionId = NextInstruction.BugReport;

            _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.From.Id, "can you explain bug ?").GetAwaiter();
        }
    }
}
