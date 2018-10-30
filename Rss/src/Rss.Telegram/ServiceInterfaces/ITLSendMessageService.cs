namespace Rss.Telegram.ServiceInterfaces
{
    public interface ITLSendMessageService
    {
        void SendMessage(string channel, string message);
    }
}
