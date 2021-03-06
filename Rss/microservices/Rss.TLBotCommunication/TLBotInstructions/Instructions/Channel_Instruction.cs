﻿using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Channel_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryRssService _queryRssService;
        public Channel_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
        }

        public void Execute()
        {
            Response<IList<RssEntity>> rss = _queryRssService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id);
            InlineKeyboardButton[][] keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];
            keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];

            for (int i = 0; i < rss.ResponseData.Count; i++)
                keyboardButtonsListrss[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(rss.ResponseData[i].AliasName, string.Concat("Add_Channel_Rss_", rss.ResponseData[i].Id)) };

            keyboardButtonsListrss[rss.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "last step to attach rss to channel. Choose one of the below.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrss)).GetAwaiter();

            SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).ChannelId = Convert.ToInt32(_callbackQueryEventArgs.CallbackQuery.Data.Substring(8, _callbackQueryEventArgs.CallbackQuery.Data.Length - 8));
        }
    }
}
