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
    public class ListChannelsInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly IQueryChannelService _queryChannelService;
        public ListChannelsInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _queryChannelService = botPlatform.Resolve<IQueryChannelService>();
        }

        public void Execute()
        {
            Response<IList<ChannelEntity>> channels = _queryChannelService.GetList(CDO.Enums.Chat.ChatType.Channel);
            InlineKeyboardButton[][] keyboardButtonsListChannels = new InlineKeyboardButton[channels.ResponseData.Count + 1][];
            keyboardButtonsListChannels = new InlineKeyboardButton[channels.ResponseData.Count + 1][];

            for (int i = 0; i < channels.ResponseData.Count; i++)
                keyboardButtonsListChannels[i] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(channels.ResponseData[i].Name, string.Concat("Channel_", channels.ResponseData[i].Name)) };

            keyboardButtonsListChannels[channels.ResponseData.Count] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
            _callbackQueryEventArgs.CallbackQuery.From.Id,
            _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
            "to add rss to channel choose one.",
            replyMarkup: new InlineKeyboardMarkup(keyboardButtonsListChannels)).GetAwaiter();
        }
    }
}
