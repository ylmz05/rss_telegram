namespace Rss.Domain.Entities
{
    public class RssEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string AliasName { get; set; }
        public long UserId { get; set; }
    }
}