using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class Remove_Rss_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandRssChatRelationService _commandRssChatService;
        public Remove_Rss_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandRssChatService = botPlatform.Resolve<ICommandRssChatRelationService>();
        }
        public void Execute()
        {
            if (!string.IsNullOrEmpty(SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Url))
            {

                Response<int> response = _commandRssChatService.Remove(_callbackQueryEventArgs.CallbackQuery.From.Id, _callbackQueryEventArgs.CallbackQuery.Data.Substring(11, _callbackQueryEventArgs.CallbackQuery.Data.Length - 11), SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Url);
                if (response.Type.Equals(ResponseType.NotFound))
                    _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "chosen rss is not found.");
                else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "successfully done.");
                SessionHelper.GetSession(_callbackQueryEventArgs.CallbackQuery.From.Id).Url = string.Empty;
            }
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "choose rss first.");
        }
    }
}
