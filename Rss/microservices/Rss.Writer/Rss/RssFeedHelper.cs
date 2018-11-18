using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Rss.Writer.Rss
{
    public class RssFeedHelper
    {
        private const string FILE_NAME = "../rssfeed/localrss";

        static RssFeedHelper()
        {
            CreateIfNotExists();
        }

        public static SyndicationFeed CreateRss20()
        {
            SyndicationFeed feed = new SyndicationFeed();
            feed.Authors.Add(new SyndicationPerson("tunahan.yilmaz94@gmail.com"));
            feed.Categories.Add(new SyndicationCategory("economy news"));
            return feed;
        }
        public static void CreateIfNotExists()
        {
            SyndicationFeed feed = CreateRss20();

            if (!File.Exists(FILE_NAME))
            {
                using (XmlWriter writer = XmlWriter.Create(FILE_NAME))
                    feed.SaveAsRss20(writer);
            }
        }
        public static void Save(string title, string content)
        {
            SyndicationFeed feed = Load();
            feed.Items = new List<SyndicationItem>(feed.Items.Take(29))
            {
                new SyndicationItem()
                {
                    PublishDate = DateTime.Now,
                    Title = new TextSyndicationContent(title),
                    Summary = new TextSyndicationContent("webscrapping"),
                    Content = SyndicationContent.CreatePlaintextContent(content)
                }
            };

            using (XmlWriter writer = XmlWriter.Create(FILE_NAME))
                feed.SaveAsRss20(writer);
        }
        public static bool IsContentExists(string content)
        {
            SyndicationFeed feed = Load();
            if (string.IsNullOrEmpty(feed.Items.Select(x => ((TextSyndicationContent)x.Content).Text).Where(x => x.Equals(content)).FirstOrDefault()))
                return true;

            return false;
        }
        private static SyndicationFeed Load()
        {
            using (XmlReader reader = XmlReader.Create(FILE_NAME))
                return SyndicationFeed.Load(reader);
        }
    }
}
