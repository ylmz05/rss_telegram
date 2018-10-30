using Rss.Application.Services.Interfaces.Commands;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.Instructions
{
    public class ChatMembersAddedInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        private readonly ICommandChannelService _commandChannelService;
        public ChatMembersAddedInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
            _commandChannelService = botPlatform.Resolve<ICommandChannelService>();
        }
        public void Execute()
        {
            _commandChannelService.Add(new ChannelEntity() { ChatId = _messageEventArgs.Message.Chat.Id, Name = string.Concat(_messageEventArgs.Message.Chat.Username, "/", _messageEventArgs.Message.Chat.Title), UserId = _messageEventArgs.Message.From.Id, Type = (short)CDO.Enums.Chat.ChatType.Group });
            _telegramBotClient.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "group added to list.").GetAwaiter();
        }
    }
}
