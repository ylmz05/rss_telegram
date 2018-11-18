using Rss.CDO.Enums.TLBot;

namespace Rss.TLBotCommunication.UserSession
{
    public class Session
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public long PrivateChatId { get; set; }
        public string Code { get; set; }
        public int ChannelId { get; set; }
        public int UrlId { get; set; }
        public string Payload { get; set; }
        public NextInstruction InstructionId { get; set; }
    }
}
