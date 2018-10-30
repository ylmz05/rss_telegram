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
    public class RemoveChannel_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandChannelService _commandChannelService;
        public RemoveChannel_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandChannelService = botPlatform.Resolve<ICommandChannelService>();
        }
        public void Execute()
        {
            Response<int> response = _commandChannelService.Remove(new ChannelEntity() { UserId = _callbackQueryEventArgs.CallbackQuery.From.Id, Name = _callbackQueryEventArgs.CallbackQuery.Data.Substring(14, _callbackQueryEventArgs.CallbackQuery.Data.Length - 14) });
            if (response.Type.Equals(ResponseType.NotFound))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "channel is not found.");
            else if (response.Type.Equals(ResponseType.Success))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "channel is removed.");
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "channel is not removed.");
        }
    }
}
