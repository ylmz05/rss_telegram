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
        private readonly string _rss;
        private readonly DateTimeOffset _publishedDate;
        public RssConnector(string rss, DateTimeOffset publishedDate)
        {
            _rss = rss;
            _publishedDate = publishedDate;
        }

        public IList<SyndicationItem> GetLatestUpdates()
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(_rss))
                {
                    var items = SyndicationFeed.Load(reader);
                    return items.Items.Where(x => x.PublishDate > _publishedDate).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("Error: ", ex.Message, "\nUri: ", _rss));
                return new List<SyndicationItem>();
            }
        }
    }
}
