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
    public class ListRssInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryRssService _queryRssService;
        public ListRssInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
        }

        public void Execute()
        {
            Response<IList<RssEntity>> rss = _queryRssService.GetList();
            InlineKeyboardButton[][] keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];
            keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];

            for (int i = 0; i < rss.ResponseData.Count; i++)
                keyboardButtonsListrss[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(rss.ResponseData[i].Url, string.Concat("Rss_", rss.ResponseData[i].Url)) };

            keyboardButtonsListrss[rss.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "in order to list atached groups and channels, choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrss)).GetAwaiter();
        }
    }
}
