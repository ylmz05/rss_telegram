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
        private const string FILE_NAME_ECONOMY = "../rssfeed/localrss";
        private const string FILE_NAME_BUGREPORT = "../rssfeed/bugreport";

        static RssFeedHelper()
        {
            CreateIfNotExists();
        }

        public static SyndicationFeed CreateRss20(string category)
        {
            SyndicationFeed feed = new SyndicationFeed();
            feed.Authors.Add(new SyndicationPerson("tunahan.yilmaz94@gmail.com"));
            feed.Categories.Add(new SyndicationCategory(category));
            return feed;
        }
        public static void CreateIfNotExists()
        {
            SyndicationFeed feed = CreateRss20("economy news");

            if (!File.Exists(FILE_NAME_ECONOMY))
            {
                using (XmlWriter writer = XmlWriter.Create(FILE_NAME_ECONOMY))
                    feed.SaveAsRss20(writer);
            }

            feed = CreateRss20("bug report");

            if (!File.Exists(FILE_NAME_BUGREPORT))
            {
                using (XmlWriter writer = XmlWriter.Create(FILE_NAME_BUGREPORT))
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

            using (XmlWriter writer = XmlWriter.Create(FILE_NAME_ECONOMY))
                feed.SaveAsRss20(writer);
        }

        public static void SaveBugReport(string title, string content)
        {
            SyndicationFeed feed = Load();
            feed.Items = new List<SyndicationItem>(feed.Items.Take(29))
            {
                new SyndicationItem()
                {
                    PublishDate = DateTime.Now,
                    Title = new TextSyndicationContent(title),
                    Summary = new TextSyndicationContent("bugreport"),
                    Content = SyndicationContent.CreatePlaintextContent(content)
                }
            };
            
            using (XmlWriter writer = XmlWriter.Create(FILE_NAME_BUGREPORT))
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
            using (XmlReader reader = XmlReader.Create(FILE_NAME_ECONOMY))
                return SyndicationFeed.Load(reader);
        }
    }
}
