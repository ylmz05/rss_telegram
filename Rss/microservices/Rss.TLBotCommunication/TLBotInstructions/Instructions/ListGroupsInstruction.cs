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
    public class ListGroupsInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryChannelService _queryChannelService;
        public ListGroupsInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> groups = _queryChannelService.GetList(CDO.Enums.Chat.ChatType.Group);
            InlineKeyboardButton[][] keyboardButtonsListGroups = new InlineKeyboardButton[groups.ResponseData.Count + 1][];
            keyboardButtonsListGroups = new InlineKeyboardButton[groups.ResponseData.Count + 1][];

            for (int i = 0; i < groups.ResponseData.Count; i++)
                keyboardButtonsListGroups[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(groups.ResponseData[i].Name, string.Concat("Group_", groups.ResponseData[i].Name)) };

            keyboardButtonsListGroups[groups.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "to add rss to group choose one.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListGroups)).GetAwaiter();
        }
    }
}
