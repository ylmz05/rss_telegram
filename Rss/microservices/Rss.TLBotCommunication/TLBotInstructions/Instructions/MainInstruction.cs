using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class MainInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public MainInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }

        public void Execute()
        {
            InlineKeyboardButton[][] keyboardButtons = new InlineKeyboardButton[1][];

            keyboardButtons[0] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Channels"), InlineKeyboardButton.WithCallbackData("Groups"), InlineKeyboardButton.WithCallbackData("Rss") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "List/Edit channels or groups or rss. Choose one of the below.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtons)).GetAwaiter();
        }
    }
}
