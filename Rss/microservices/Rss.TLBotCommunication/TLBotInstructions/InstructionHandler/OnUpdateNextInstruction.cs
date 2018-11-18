using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Enums.TLBot;
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
                Session session = SessionHelper.IsCodeExist(_updateEventArgs.Update.ChannelPost.Text);

                if (session != null && session.InstructionId.Equals(NextInstruction.WaitingCode))
                {
                    Response<int> response = _commandChannelService.Add(new ChannelEntity() { ChatId = _updateEventArgs.Update.ChannelPost.Chat.Id, Name = string.Concat(_updateEventArgs.Update.ChannelPost.Chat.Username, "/", _updateEventArgs.Update.ChannelPost.Chat.Title), UserId = session.UserId, Type = (short)CDO.Enums.Chat.ChatType.Channel });
                    if (response.Type.Equals(ResponseType.Success))
                    {
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is added to list.").GetAwaiter();
                        session.InstructionId = NextInstruction.CodeValidated;
                        TimerHelper.DataReceived();
                    }
                    else if (response.Type.Equals(ResponseType.AlreadyExist))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is already exist in list.").GetAwaiter();
                    else
                    {
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "channel is not added to list. User is not found.").GetAwaiter();
                        session.InstructionId = NextInstruction.CodeNotValidated;
                        TimerHelper.DataReceived();
                    }
                }
                else if(session != null && !session.InstructionId.Equals(NextInstruction.WaitingCode))
                {
                    if (session.InstructionId.Equals(NextInstruction.CodeNotValidated))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "code not validated. call Add Channel again. [SERVER_EXCEPTION]").GetAwaiter();
                    else if (session.InstructionId.Equals(NextInstruction.CodeNotValidatedTimeOut))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "code not validated. call Add Channel again. [TIMEOUT]").GetAwaiter();
                    else if (session.InstructionId.Equals(NextInstruction.CodeValidated))
                        _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "code is already used. please call Add Channel again.").GetAwaiter();
                }
                else
                    _telegramBotClient.SendTextMessageAsync(_updateEventArgs.Update.ChannelPost.Chat.Id, "wrong code is entered. try again.").GetAwaiter();
            }
        }
    }
}
