using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class DefaultInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        private readonly ICommandRssService _commandRssService;
        public DefaultInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
            _commandRssService = botPlatform.Resolve<ICommandRssService>();
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_messageEventArgs.Message.From.Id);
            if (session.CanMessageReceive)
                if (RssHelper.IsRss(_messageEventArgs.Message.Text.Trim()))
                {
                    Response<int> response = _commandRssService.Add(new RssEntity() { Url = _messageEventArgs.Message.Text.Trim(), UserId = _messageEventArgs.Message.From.Id });
                    if (response.Type.Equals(ResponseType.Success))
                        _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss added to list").GetAwaiter();
                    else if (response.Type.Equals(ResponseType.AlreadyExist))
                        _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss is already exist in list.").GetAwaiter();

                    session.CanMessageReceive = false;
                }
                else _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "rss is not found. please enter valid url.").GetAwaiter();
        }
    }
}
