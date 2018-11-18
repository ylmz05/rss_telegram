using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IQueryRssChatRelationService _queryRssChatRelationService;

        public RemoveRssInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssService = botPlatform.Resolve<IQueryRssService>();
            _queryRssChatRelationService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }

        public void Execute()
        {
            Response<IList<RssEntity>> rssRemove = _queryRssService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id);
            InlineKeyboardButton[][] keyboardButtonsListrssRemove = new InlineKeyboardButton[rssRemove.ResponseData.Count + 1][];
            keyboardButtonsListrssRemove = new InlineKeyboardButton[rssRemove.ResponseData.Count + 1][];

            for (int i = 0; i < rssRemove.ResponseData.Count; i++)
            {
                int count = _queryRssChatRelationService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id, rssRemove.ResponseData[i].AliasName, CDO.Enums.Chat.ListChatRelation.ByAliasName).ResponseData.Count;
                if (count.Equals(0))
                    keyboardButtonsListrssRemove[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(rssRemove.ResponseData[i].AliasName, string.Concat("RemoveRss_", rssRemove.ResponseData[i].Id)) };
            }

            keyboardButtonsListrssRemove[rssRemove.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "you can only remove rss that is not attached to any group or channel. listed below that is not attached to any group or channel.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrssRemove.Where(x => x != null).ToArray())).GetAwaiter();
        }
    }
}