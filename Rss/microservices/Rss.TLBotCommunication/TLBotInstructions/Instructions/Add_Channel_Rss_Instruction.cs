using Rss.Application.Services.Interfaces.Commands;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Add_Channel_Rss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandRssChatRelationService _commandRssChatService;
        private readonly IQueryRssService _queryRssService;
        private readonly IQueryChannelService _queryChannelService;
        public Add_Channel_Rss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandRssChatService = botPlatform.Resolve<ICommandRssChatRelationService>();
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id);
            if (!session.ChannelId.Equals(0))
            {
                ChannelEntity channel = _queryChannelService.Get(session.ChannelId).ResponseData;
                RssEntity rss = _queryRssService.Get(Convert.ToInt32(_callbackQueryEventArgs.CallbackQuery.Data.Substring(16, _callbackQueryEventArgs.CallbackQuery.Data.Length - 16))).ResponseData;
                Response<int> response = _commandRssChatService.Add(new RssChatRelationEntity() {Url = rss.Url, ChatId = 0, AliasName = rss.AliasName, UserId = session.UserId }, channel.Name);
                if (response.Type.Equals(ResponseType.Success))
                    _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "selected rss is attached to channel.");
                else if (response.Type.Equals(ResponseType.AlreadyExist))
                    _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "chosen rss is already attached to selected channel.");
                else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "null data [SESSION ERROR]");
            }
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "choose group/channel first.");
        }
    }
}
