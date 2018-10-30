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
    public class RemoveChannelInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryChannelService _queryChannelService;

        public RemoveChannelInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> channelsRemove = _queryChannelService.GetList(CDO.Enums.Chat.ChatType.Channel);
            InlineKeyboardButton[][] keyboardButtonsListChannelsRemove = new InlineKeyboardButton[channelsRemove.ResponseData.Count + 1][];
            keyboardButtonsListChannelsRemove = new InlineKeyboardButton[channelsRemove.ResponseData.Count + 1][];

            for (int i = 0; i < channelsRemove.ResponseData.Count; i++)
                keyboardButtonsListChannelsRemove[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(channelsRemove.ResponseData[i].Name, string.Concat("RemoveChannel_", channelsRemove.ResponseData[i].Name)) };

            keyboardButtonsListChannelsRemove[channelsRemove.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "to remove channel, choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListChannelsRemove)).GetAwaiter();
        }
    }
}
