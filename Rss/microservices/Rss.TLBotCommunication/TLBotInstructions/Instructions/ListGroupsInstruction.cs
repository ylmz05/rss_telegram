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
        private readonly IQueryRssChatRelationService _queryRssChatRelationService;
        public ListGroupsInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
            _queryRssChatRelationService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> channels = _queryChannelService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id, CDO.Enums.Chat.ChatType.Group);
            InlineKeyboardButton[][] keyboardButtonsListChannels = new InlineKeyboardButton[channels.ResponseData.Count + 1][];
            keyboardButtonsListChannels = new InlineKeyboardButton[channels.ResponseData.Count + 1][];

            for (int i = 0; i < channels.ResponseData.Count; i++)
            {
                int count = _queryRssChatRelationService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id, channels.ResponseData[i].Name, CDO.Enums.Chat.ListChatRelation.ByChatName).ResponseData.Count;
                keyboardButtonsListChannels[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(string.Concat(channels.ResponseData[i].Name, " (", count, ")"), string.Concat("Group_", channels.ResponseData[i].Id)) };
            }

            keyboardButtonsListChannels[channels.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "you can attach rss to selected group from rss list.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListChannels)).GetAwaiter();
        }
    }
}
