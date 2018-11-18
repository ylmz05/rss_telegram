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
    public class RemoveGroupInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryChannelService _queryChannelService;
        private readonly IQueryRssChatRelationService _queryRssChatRelationService;

        public RemoveGroupInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
            _queryRssChatRelationService = botPlatform.Resolve<IQueryRssChatRelationService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> channelsRemove = _queryChannelService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id, CDO.Enums.Chat.ChatType.Group);
            InlineKeyboardButton[][] keyboardButtonsListChannelsRemove = new InlineKeyboardButton[channelsRemove.ResponseData.Count + 1][];
            keyboardButtonsListChannelsRemove = new InlineKeyboardButton[channelsRemove.ResponseData.Count + 1][];

            for (int i = 0; i < channelsRemove.ResponseData.Count; i++)
            {
                int count = _queryRssChatRelationService.GetList(_callbackQueryEventArgs.CallbackQuery.From.Id, channelsRemove.ResponseData[i].Name, CDO.Enums.Chat.ListChatRelation.ByChatName).ResponseData.Count;
                if (count.Equals(0))
                    keyboardButtonsListChannelsRemove[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(channelsRemove.ResponseData[i].Name, string.Concat("RemoveGroup_", channelsRemove.ResponseData[i].Name)) };
            }

            keyboardButtonsListChannelsRemove[channelsRemove.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "to remove channel, choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListChannelsRemove.Where(x => x != null).ToArray())).GetAwaiter();
        }
    }
}
