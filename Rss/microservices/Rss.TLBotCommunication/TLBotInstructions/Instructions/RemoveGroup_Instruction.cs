using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class RemoveGroup_Instruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly ICommandChannelService _commandChannelService;
        public RemoveGroup_Instruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _commandChannelService = botPlatform.Resolve<ICommandChannelService>();
        }
        public void Execute()
        {
            Response<int> response = _commandChannelService.Remove(new ChannelEntity() { UserId = _callbackQueryEventArgs.CallbackQuery.From.Id, Name = _callbackQueryEventArgs.CallbackQuery.Data.Substring(12, _callbackQueryEventArgs.CallbackQuery.Data.Length - 12) });
            if (response.Type.Equals(ResponseType.NotFound))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "group is not found.");
            else if (response.Type.Equals(ResponseType.Success))
                _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "group is removed.");
            else _telegramBotClient.SendTextMessageAsync(_callbackQueryEventArgs.CallbackQuery.From.Id, "group is not removed.");
        }
    }
}
