using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Add_Channel_Rss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandRssChatRelationService _commandRssChatService;
        public Add_Channel_Rss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandRssChatService = botPlatform.Resolve<ICommandRssChatRelationService>();
        }
        public void Execute()
        {
            if (!string.IsNullOrEmpty(SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Channel))
            {
                Response<int> response = _commandRssChatService.Add(new RssChatRelationEntity() { ChatId = 0, Url = _callbackQueryEventArgs.CallbackQuery.Data.Substring(16, _callbackQueryEventArgs.CallbackQuery.Data.Length - 16), UserId = _callbackQueryEventArgs.CallbackQuery.From.Id, Name = SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Channel }, SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Channel);
                if (response.Type.Equals(ResponseType.AlreadyExist))
                    _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "chosen rss is already attached to selected channel.");
                else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "succesfully done.");
                SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Channel = string.Empty;
            }
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "choose group/channel first.");
        }
    }
}
