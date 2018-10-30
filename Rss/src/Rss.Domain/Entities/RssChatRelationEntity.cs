using Rss.Domain.EntityInterfaces;

namespace Rss.Domain.Entities
{
    public class RssChatRelationEntity : IEntity
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Url { get; set; }
        public long ChatId { get; set; }
        public string Name { get; set; }
    }
}
