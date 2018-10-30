namespace Rss.Domain.Entities
{
    public class BotEntity
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public long UserId { get; set; }
    }
}