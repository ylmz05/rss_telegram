namespace Rss.Domain.Entities
{
    public class ChannelEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long ChatId { get; set; }

        public long UserId { get; set; }

        public short Type { get; set; }
    }
}
