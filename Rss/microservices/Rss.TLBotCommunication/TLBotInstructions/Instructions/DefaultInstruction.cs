using Rss.Application.Services.Interfaces.Commands;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Enums.TLBot;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Rss.Writer.Rss;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class DefaultInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        private readonly ICommandRssService _commandRssService;
        private readonly IQueryRssService _queryRssService;
        public DefaultInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
            _commandRssService = botPlatform.Resolve<ICommandRssService>();
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_messageEventArgs.Message.From.Id);
            if (session.InstructionId.Equals(NextInstruction.None))
                return;

            if (session.InstructionId.Equals(NextInstruction.AddRss))
            {
                if (RssHelper.IsRss(_messageEventArgs.Message.Text.Trim()))
                {
                    Response<RssEntity> Validation = _queryRssService.Get(_messageEventArgs.Message.From.Id, _messageEventArgs.Message.Text.Trim());
                    if (Validation.ResponseData == null)
                    {
                        session.InstructionId = NextInstruction.AddAlias;
                        _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, $"enter alias name for the rss.").GetAwaiter();
                        session.Payload = _messageEventArgs.Message.Text.Trim();
                    }
                    else
                        _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss is already exist in list.").GetAwaiter();
                }
                else
                    _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss is not found. please enter valid url.").GetAwaiter();
            }
            else if(session.InstructionId.Equals(NextInstruction.AddAlias))
            {
                Response<int> response = _commandRssService.Add(new RssEntity() { Url = session.Payload, AliasName = _messageEventArgs.Message.Text.Trim(), UserId = _messageEventArgs.Message.From.Id });
                if (response.Type.Equals(ResponseType.Success))
                {
                    _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss added to list").GetAwaiter();
                    session.InstructionId = NextInstruction.None;
                }
                else if (response.Type.Equals(ResponseType.AlreadyExist))
                    _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "alias name is already exist. please enter unique name").GetAwaiter();

            }
            else if (session.InstructionId.Equals(NextInstruction.BugReport))
            {
                RssFeedHelper.SaveBugReport($"bug from {_messageEventArgs.Message.Chat.Id}", _messageEventArgs.Message.Text);
                _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "thank you for your report.").GetAwaiter();
                session.InstructionId = NextInstruction.None;
            }
        }
    }
}
