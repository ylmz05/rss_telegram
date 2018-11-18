using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class RemoveRss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandRssService _commandRssService;
        public RemoveRss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandRssService = botPlatform.Resolve<ICommandRssService>();
        }
        public void Execute()
        {
            Response<int> response = _commandRssService.Remove(new RssEntity() { UserId = _callbackQueryEventArgs.CallbackQuery.From.Id, AliasName = _callbackQueryEventArgs.CallbackQuery.Data.Substring(10, _callbackQueryEventArgs.CallbackQuery.Data.Length - 10) });
            if (response.Type.Equals(ResponseType.NotFound))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "rss is not found.");
            else if (response.Type.Equals(ResponseType.Success))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "rss is removed.");
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "rss is not removed.");
        }
    }
}
