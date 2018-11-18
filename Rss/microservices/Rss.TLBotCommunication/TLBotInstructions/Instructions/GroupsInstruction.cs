using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class GroupsInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        public GroupsInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
        }

        public void Execute()
        {
            InlineKeyboardButton[][] keyboardButtonsGroups = new InlineKeyboardButton[2][];
            keyboardButtonsGroups[0] = new InlineKeyboardButton[] 
            {
                InlineKeyboardButton.WithCallbackData("List Groups"),
                InlineKeyboardButton.WithCallbackData("Remove Group"),
                InlineKeyboardButton.WithCallbackData("Add Group")
            };
            keyboardButtonsGroups[1] = new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Back to Home") };

            _telegramBotClient.EditMessageTextAsync(
                _callbackQueryEventArgs.CallbackQuery.From.Id,
                _callbackQueryEventArgs.CallbackQuery.Message.MessageId,
                "manage your groups.",
                replyMarkup: new InlineKeyboardMarkup(keyboardButtonsGroups)).GetAwaiter();
        }
    }
}
