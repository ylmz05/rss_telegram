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
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Remove_Rss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandRssChatRelationService _commandRssChatService;
        private readonly IQueryRssChatRelationService _queryRssChatService;
        public Remove_Rss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandRssChatService = botPlatform.Resolve<ICommandRssChatRelationService>();
            _queryRssChatService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }
        public void Execute()
        {
            Session session = SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id);
            if (session.InstructionId.Equals(NextInstruction.RemoveChannelGroupFromRss))
            {
                Response<RssChatRelationEntity> rss = _queryRssChatService.Get(Convert.ToInt32(_callbackQueryEventArgs.CallbackQuery.Data.Substring(11, _callbackQueryEventArgs.CallbackQuery.Data.Length - 11)));
                Response<int> response = _commandRssChatService.Remove(rss.ResponseData);
                if (response.ResponseData.Equals(0) || response.Type.Equals(ResponseType.NotFound))
                    _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "chosen rss is not found.");
                else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "successfully done.");
                session.UrlId = 0;
            }
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "choose rss first.");
        }
    }
}
