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
        private const string FILE_NAME = @"\localrss";

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
            string fullPath = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FILE_NAME);

            SyndicationFeed feed = CreateRss20();

            if (!File.Exists(fullPath))
            {
                using (XmlWriter writer = XmlWriter.Create(fullPath))
                    feed.SaveAsRss20(writer);
            }
        }
        public static void Save(string title, string content)
        {
            string fullPath = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FILE_NAME);

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

            using (XmlWriter writer = XmlWriter.Create(fullPath))
                feed.SaveAsRss20(writer);
        }
        public static bool IsContentExists(string content)
        {
            SyndicationFeed feed = Load();
            if (string.IsNullOrEmpty(feed.Items.Select(x => ((TextSyndicationContent)x.Content).Text).Where(x => x.Equals(content)).FirstOrDefault()))
                return false;

            return true;
        }
        private static SyndicationFeed Load()
        {
            string fullPath = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FILE_NAME);
            using (XmlReader reader = XmlReader.Create(fullPath))
                return SyndicationFeed.Load(reader);
        }
    }
}
