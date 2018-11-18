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
        private readonly IQueryRssChatRelationService _queryRssChatRelationService;
        public ListRssInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
            _queryRssChatRelationService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }

        public void Execute()
        {
            Response<IList<RssEntity>> rss = _queryRssService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id);
            if (!rss.ResponseData.Count.Equals(0))
            {
                InlineKeyboardButton[][] keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];
                keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];

                for (int i = 0; i < rss.ResponseData.Count; i++)
                {
                    int count = _queryRssChatRelationService.GetList(rss.ResponseData[i].AliasName).ResponseData.Count;
                    keyboardButtonsListrss[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(string.Concat(rss.ResponseData[i].AliasName, " (", count, ")"), string.Concat("Rss_", rss.ResponseData[i].AliasName)) };
                }

                keyboardButtonsListrss[rss.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

                _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "in order to list atached groups and channels, choose one.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrss)).GetAwaiter();
            }
            else
            {
                InlineKeyboardButton[][] keyboardButtonsListrss = new InlineKeyboardButton[2][];
                keyboardButtonsListrss = new InlineKeyboardButton[2][];

                keyboardButtonsListrss[0] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Add Rss") };
                keyboardButtonsListrss[1] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

                _telegramBotClient.EditMessageTextAsync(
                  _callbackQueryEventArgs.CallbackQuery.From.Id,
                  _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                  "you don't have any rss link.",
                  replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrss)).GetAwaiter();
            }

         
        }
    }
}
