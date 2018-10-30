using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.Helpers;
using Rss.TLBotCommunication.TLBotInstructions.Instructions;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using Rss.TLBotCommunication.UserSession;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication.TLBotInstructions.InstructionHandler
{
    public class OnCallBackQueryNextInstruction : IInstruction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly CallbackQueryEventArgs _callbackQueryEventArgs;
        private readonly BotPlatform _botPlatform;
        public OnCallBackQueryNextInstruction(ITelegramBotClient telegramBotClient, CallbackQueryEventArgs callbackQueryEventArgs, BotPlatform botPlatform)
        {
            _telegramBotClient = telegramBotClient;
            _callbackQueryEventArgs = callbackQueryEventArgs;
            _botPlatform = botPlatform;
        }

        public void Execute()
        {
            SessionHelper.AddSession(new Session() { Name = _callbackQueryEventArgs.CallbackQuery.From.Username, UserId = _callbackQueryEventArgs.CallbackQuery.From.Id, PrivateChatId = _callbackQueryEventArgs.CallbackQuery.From.Id, CanAddChannel = true });

            switch (_callbackQueryEventArgs.CallbackQuery.Data)
            {
                case "Channels":
                    IInstruction channelsInstruction = new ChannelsInstruction(_telegramBotClient, _callbackQueryEventArgs);
                    channelsInstruction.Execute();
                    break;
                case "Groups":
                    IInstruction groupsInstruction = new GroupsInstruction(_telegramBotClient, _callbackQueryEventArgs);
                    groupsInstruction.Execute();
                    break;
                case "Rss":
                    IInstruction rssInstruction = new RssInstruction(_telegramBotClient, _callbackQueryEventArgs);
                    rssInstruction.Execute();
                    break;
                case "List Groups":
                    IInstruction listGroupsInstruction = new ListGroupsInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    listGroupsInstruction.Execute();
                    break;
                case "List Channels":
                    IInstruction listChannelsInstruction = new ListChannelsInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    listChannelsInstruction.Execute();
                    break;
                case "List Rss":
                    IInstruction listRssInstruction = new ListRssInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    listRssInstruction.Execute();
                    break;
                case "Main":
                    IInstruction mainInstruction = new MainInstruction(_telegramBotClient, _callbackQueryEventArgs);
                    mainInstruction.Execute();
                    break;
                case "Add Rss":
                    IInstruction addRssInstruction = new AddRssInstruction(_telegramBotClient, _callbackQueryEventArgs);
                    addRssInstruction.Execute();
                    break;
                case "Remove Rss":
                    IInstruction removeRssInstruction = new RemoveRssInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    removeRssInstruction.Execute();
                    break;
                case "Remove Channel":
                    IInstruction removeChannelInstruction = new RemoveChannelInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    removeChannelInstruction.Execute();
                    break;
                case "Remove Group":
                    IInstruction removeGroupInstruction = new RemoveGroupInstruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                    removeGroupInstruction.Execute();
                    break;
            }

            if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Channel_"))
            {
                IInstruction channel_Instruction = new Channel_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                channel_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Group_"))
            {
                IInstruction group_Instruction = new Group_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                group_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Rss_"))
            {
                IInstruction rss_Instruction = new Rss_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                rss_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("RemoveRss_"))
            {
                IInstruction removeRss_Instruction = new RemoveRss_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                removeRss_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("RemoveChannel_"))
            {
                IInstruction removeChannel_Instruction = new RemoveChannel_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                removeChannel_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("RemoveGroup_"))
            {
                IInstruction removeGroup_Instruction = new RemoveGroup_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                removeGroup_Instruction.Execute();
            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Add_Channel_Rss_"))
            {
                IInstruction add_Channel_Rss_Instruction = new Add_Channel_Rss_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                add_Channel_Rss_Instruction.Execute();

            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Add_Group_Rss_"))
            {
                IInstruction add_Group_Rss_Instruction = new Add_Group_Rss_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                add_Group_Rss_Instruction.Execute();

            }
            else if (_callbackQueryEventArgs.CallbackQuery.Data.StartsWith("Remove_Rss_"))
            {
                IInstruction remove_Rss_Instruction = new Remove_Rss_Instruction(_telegramBotClient, _callbackQueryEventArgs, _botPlatform);
                remove_Rss_Instruction.Execute();

            }
        }
    }
}
