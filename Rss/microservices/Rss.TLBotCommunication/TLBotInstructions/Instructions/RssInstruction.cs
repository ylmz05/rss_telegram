using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class RssInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public RssInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }
        public void Execute()
        {
            InlineKeyboardButton[][] keyboardButtonsRss = new InlineKeyboardButton[2][];
            keyboardButtonsRss[0] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("List Rss"), InlineKeyboardButton.WithCallbackData("Remove Rss"), InlineKeyboardButton.WithCallbackData("Add Rss") };
            keyboardButtonsRss[1] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Main") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "List/Edit Rss.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsRss)).GetAwaiter();
        }
    }
}
