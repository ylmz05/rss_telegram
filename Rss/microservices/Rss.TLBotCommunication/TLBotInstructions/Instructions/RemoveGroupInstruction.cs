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
    public class RemoveGroupInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryChannelService _queryChannelService;

        public RemoveGroupInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> groupsRemove = _queryChannelService.GetList(CDO.Enums.Chat.ChatType.Group);
            InlineKeyboardButton[][] keyboardButtonsListGroupsRemove = new InlineKeyboardButton[groupsRemove.ResponseData.Count + 1][];
            keyboardButtonsListGroupsRemove = new InlineKeyboardButton[groupsRemove.ResponseData.Count + 1][];

            for (int i = 0; i < groupsRemove.ResponseData.Count; i++)
                keyboardButtonsListGroupsRemove[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(groupsRemove.ResponseData[i].Name, string.Concat("RemoveGroup_", groupsRemove.ResponseData[i].Name)) };

            keyboardButtonsListGroupsRemove[groupsRemove.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "to remove group, choose one.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListGroupsRemove)).GetAwaiter();
        }
    }
}
