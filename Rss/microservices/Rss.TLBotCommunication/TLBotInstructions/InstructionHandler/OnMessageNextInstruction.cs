using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Instructions;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Rss.TLBotCommunication.TLBotInstructions.InstructionHandler
{
    public class OnMessageNextInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly MessageEventArgs _messageEventArgs;
        private readonly BotPlatform _botPlatform;
        public OnMessageNextInstruction(ITelegramBotClient telegramBotClient, MessageEventArgs messageEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _messageEventArgs = messageEventArgs;
            _botPlatform = botPlatform;
        }
        public void Execute()
        {
            if (_messageEventArgs.Message.Type.Equals(MessageType.ChatMembersAdded))
            {
                IInstruction chatMembersAddedInstruction = new ChatMembersAddedInstruction(_telegramBotClient, _messageEventArgs, _botPlatform);
                chatMembersAddedInstruction.Execute();
            }

            if (!_messageEventArgs.Message.Chat.Type.Equals(ChatType.Private))
                return;

            SessionHelper.AddSession(new Session() { Name = _messageEventArgs.Message.From.Username, UserId = _messageEventArgs.Message.From.Id, PrivateChatId = _messageEventArgs.Message.Chat.Id, CanAddChannel = true });

            switch (_messageEventArgs.Message.Text.Trim().Replace("@RssServiceBot", ""))
            {
                case "/addchannel":
                    if (SessionHelper.GetSession(_messageEventArgs.Message.From.Id).CanAddChannel)
                    {
                        IInstruction addChannelInstruction = new AddChannelInstruction(_telegramBotClient, _messageEventArgs);
                        addChannelInstruction.Execute();
                    }
                    break;
                case "/addgroup":
                    IInstruction addGroupInstruction = new AddGroupInstruction(_telegramBotClient, _messageEventArgs);
                    addGroupInstruction.Execute();
                    break;
                case "/rssedit":
                    IInstruction rssEditInstruction = new RssEditInstruction(_telegramBotClient, _messageEventArgs);
                    rssEditInstruction.Execute();
                    break;
                default:
                    IInstruction defaultInstruction = new DefaultInstruction(_telegramBotClient, _messageEventArgs, _botPlatform);
                    defaultInstruction.Execute();
                    break;
            }
        }
    }
}
