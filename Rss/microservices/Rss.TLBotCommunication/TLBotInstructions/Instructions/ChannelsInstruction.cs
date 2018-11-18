using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class ChannelsInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public ChannelsInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }

        public void Execute()
        {
            InlineKeyboardButton[][] keyboardButtonsChannels = new InlineKeyboardButton[2][];
            keyboardButtonsChannels[0] = new InlineKeyboardButton[] 
            {
                InlineKeyboardButton.WithCallbackData("List Channels"),
                InlineKeyboardButton.WithCallbackData("Remove Channel"),
                InlineKeyboardButton.WithCallbackData("Add Channel")
            };
            keyboardButtonsChannels[1] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "to get updates from rss, you can add channels and attach rss to them.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsChannels)).GetAwaiter();
        }
    }
}