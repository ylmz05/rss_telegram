using Rss.Container.App;
using Rss.Messaging.AppComponents;
using Rss.TLBotCommunication.TLBotInstructions.InstructionHandler;
using Rss.TLBotCommunication.TLBotInstructions.Interfaces;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Rss.TLBotCommunication
{
    class Program
    {
        private static TelegramBotClient _telegramBotClient = new TelegramBotClient(Environment.GetEnvironmentVariable("botToken"));
        private static BotPlatform _botplatform = new BotPlatform();

        static void Main(string[] args)
        {
            Thread.Sleep(60000);
            App app = new App(_botplatform);
            app.StartContainer();

            _telegramBotClient.OnMessage += _telegramBotClient_OnMessage;
            _telegramBotClient.OnMessageEdited += _telegramBotClient_OnMessage;
            _telegramBotClient.OnCallbackQuery += _telegramBotClient_OnCallbackQuery;
            _telegramBotClient.OnReceiveError += _telegramBotClient_OnReceiveError;
            _telegramBotClient.OnUpdate += _telegramBotClient_OnUpdate;
            _telegramBotClient.StartReceiving();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void _telegramBotClient_OnUpdate(object sender, UpdateEventArgs e)
        {
            IInstruction onUpdateNextInstruction = new OnUpdateNextInstruction(_telegramBotClient, e, _botplatform);
            onUpdateNextInstruction.Execute();

            GC.Collect();
        }
        private static void _telegramBotClient_OnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Received an error: \ncode : {0} \nmesssage : {1}", e.ApiRequestException.ErrorCode, e.ApiRequestException.Message);
        }
        private static void _telegramBotClient_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            IInstruction onCallBackQueryInstruction = new OnCallBackQueryNextInstruction(_telegramBotClient, e, _botplatform);
            onCallBackQueryInstruction.Execute();

            GC.Collect();
        }
        private static void _telegramBotClient_OnMessage(object sender, MessageEventArgs e)
        {
            IInstruction onMessageNextInstruction = new OnMessageNextInstruction(_telegramBotClient, e, _botplatform);
            onMessageNextInstruction.Execute();

            GC.Collect();
        }
    }
}
