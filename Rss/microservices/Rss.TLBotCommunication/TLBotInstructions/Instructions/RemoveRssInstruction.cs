using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class RemoveRssInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryRssService _queryRssService;

        public RemoveRssInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
        }

        public void Execute()
        {
            Response<IList<RssEntity>> rssRemove = _queryRssService.GetList();
            InlineKeyboardButton[][] keyboardButtonsListrssRemove = new InlineKeyboardButton[rssRemove.ResponseData.Count + 1][];
            keyboardButtonsListrssRemove = new InlineKeyboardButton[rssRemove.ResponseData.Count + 1][];

            for (int i = 0; i < rssRemove.ResponseData.Count; i++)
                keyboardButtonsListrssRemove[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(rssRemove.ResponseData[i].Url, string.Concat("RemoveRss_", rssRemove.ResponseData[i].Url)) };

            keyboardButtonsListrssRemove[rssRemove.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "to remove rss, choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrssRemove)).GetAwaiter();
        }
    }
}