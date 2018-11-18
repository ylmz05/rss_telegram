using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.RabbitMQ.Request;
using Rss.Domain.Entities;
using Rss.Reader.AppComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Rss.Reader.Reader
{
    public class ReaderList
    {
        public static Dictionary<RssChatRelationEntity, DateTimeOffset> RssList { get; set; }
        private static IQueryRssChatRelationService _queryRssChat;
        public static void Load(ReaderPlatform readerPlatform)
        {
            _queryRssChat = readerPlatform.Resolve<IQueryRssChatRelationService>();
            RssList = _queryRssChat.GetList().ResponseData.ToDictionary(x => x, x => DateTimeOffset.Now);
        }
        public static void SyncList(ReaderPlatform readerPlatform)
        {
            _queryRssChat = readerPlatform.Resolve<IQueryRssChatRelationService>();
            var datas = _queryRssChat.GetList().ResponseData;

            RssList = RssList.Where(x => datas.Select(y => y.Id).Contains(x.Key.Id)).ToDictionary(x => x.Key, y => y.Value);

            foreach (var item in datas)
            {
                if (RssList.Select(x => x.Key.Id).Contains(item.Id))
                    continue;
                else RssList.Add(item, DateTimeOffset.Now);
            }
        }
        public static void GetLatestNews()
        {
            foreach (var rss in RssList.ToList())
            {
                try
                {
                    RssConnector rssConnector = new RssConnector(rss.Key, rss.Value);
                    IList<SyndicationItem> items = rssConnector.GetLatestUpdates();

                    foreach (SyndicationItem item in items)
                    {
                        if (item.Links.FirstOrDefault() == null)
                            RabbitMQRequest.Create(rss.Key.ChatId, ((TextSyndicationContent)item.Content).Text);
                        else RabbitMQRequest.Create(rss.Key.ChatId, item.Links.FirstOrDefault().Uri.AbsoluteUri);
                        RssList[rss.Key] = DateTimeOffset.Now;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
