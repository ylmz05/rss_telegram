using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Rss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryRssChatRelationService _queryRssChatService;
        public Rss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryRssChatService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }

        public void Execute()
        {
            Response<IList<RssChatRelationEntity>> rss = _queryRssChatService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id ,_callbackQueryEventArgs.CallbackQuery.Data.Substring(4, _callbackQueryEventArgs.CallbackQuery.Data.Length - 4), CDO.Enums.Chat.ListChatRelation.ByAliasName);
            InlineKeyboardButton[][] keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];
            keyboardButtonsListrss = new InlineKeyboardButton[rss.ResponseData.Count + 1][];

            for (int i = 0; i < rss.ResponseData.Count; i++)
                keyboardButtonsListrss[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(rss.ResponseData[i].Name, string.Concat("Remove_Rss_", rss.ResponseData[i].Id)) };

            keyboardButtonsListrss[rss.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "in order to remove rss from channel or group, choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListrss)).GetAwaiter();

            SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).InstructionId = CDO.Enums.TLBot.NextInstruction.RemoveChannelGroupFromRss;
        }
    }
}
