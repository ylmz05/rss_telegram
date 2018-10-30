using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Rss.TLBotCommunication.TLBotInstructions.InstructionHandler
{
    public class OnUpdateNextInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly UpdateEventArgs _updateEventArgs;
        private readonly ICommandChannelService _commandChannelService;
        public OnUpdateNextInstruction(ITelegramBotClient telegramBotClient, UpdateEventArgs updateEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _updateEventArgs = updateEventArgs;
            _commandChannelService = botPlatform.Resolve<ICommandChannelService>();
        }

        public void Execute()
        {
            if (_updateEventArgs.Update.Type.Equals(UpdateType.ChannelPost))
            {
                if (!_updateEventArgs.Update.ChannelPost.Text.Trim().Length.Equals(50))
                    return;

                string enteredCode = _updateEventArgs.Update.ChannelPost.Text;
                TimerHelper.IsDataReceived = true;

                if (!RandomCodeHelper.Validate(enteredCode))
                    _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "wrong code is entered. try again.").GetAwaiter();
                else
                {
                    Session session = SessionHelper.IsCodeExist(enteredCode);

                    Response<int> response = _commandChannelService.Add(new ChannelEntity() { ChatId = _updateEventArgs.Update.ChannelPost.Chat.Id, Name = string.Concat(_updateEventArgs.Update.ChannelPost.Chat.Username, "/", _updateEventArgs.Update.ChannelPost.Chat.Title), UserId = session.UserId, Type = (short)CDO.Enums.Chat.ChatType.Channel });
                    if (response.Type.Equals(ResponseType.Success))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is added to list.").GetAwaiter();
                    else if (response.Type.Equals(ResponseType.AlreadyExist))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is already exist in list.").GetAwaiter();

                    else _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is not added to list. User is not found in sessions").GetAwaiter();

                    session.CanAddChannel = true;
                }
            }
        }
    }
}
