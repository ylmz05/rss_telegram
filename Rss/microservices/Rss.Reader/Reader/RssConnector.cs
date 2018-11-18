using Rss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Rss.Reader.Reader
{
    public class RssConnector
    {
        private readonly RssChatRelationEntity _rss;
        private readonly DateTimeOffset _publishedDate;
        public RssConnector(RssChatRelationEntity rss, DateTimeOffset publishedDate)
        {
            _rss = rss;
            _publishedDate = publishedDate;
        }

        public IList<SyndicationItem> GetLatestUpdates()
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(_rss.Url))
                {
                    var items = SyndicationFeed.Load(reader);
                    return items.Items.Where(x => x.PublishDate > _publishedDate).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("\nError: ", ex.Message, "\nUri: ", _rss.Url));
                return new List<SyndicationItem>();
            }
        }
    }
}
