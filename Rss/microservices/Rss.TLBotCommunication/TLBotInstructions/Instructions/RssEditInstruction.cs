using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class RssEditInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        public RssEditInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
        }
        public void Execute()
        {
            InlineKeyboardButton[][] keyboardButtons = new InlineKeyboardButton[1][];

            keyboardButtons[0] = new InlineKeyboardButton[] 
            {
                InlineKeyboardButton.WithCallbackData("Channels"),
                InlineKeyboardButton.WithCallbackData("Groups"),
                InlineKeyboardButton.WithCallbackData("Rss")
            };

            _telegramBotClient.SendTextMessageAsync(
                _messageEventArgs.Message.Chat.Id,
                "Manage your channels, groups and rss list.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtons)).GetAwaiter();
        }
    }
}
