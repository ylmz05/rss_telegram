namespace Rss.TLBotCommunication.UserSession
{
    public class Session
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public long PrivateChatId { get; set; }
        public string Code { get; set; }
        public bool CanMessageReceive { get; set; }
        public bool CanAddChannel { get; set; }
        public string Channel { get; set; }
        public string Url { get; set; }
    }
}
