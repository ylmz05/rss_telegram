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
            Parallel.ForEach(RssList.GroupBy(x => x.Key.Url).Select(y => new { url = y.Key, chatIds = y.Select(s => new { chatId = s.Key.ChatId, date = s.Value }) }).ToList(), rss =>
            {
                try
                {
                    RssConnector rssConnector = new RssConnector(rss.url, rss.chatIds.FirstOrDefault().date);
                    IList<SyndicationItem> items = rssConnector.GetLatestUpdates();

                    foreach (SyndicationItem item in items)
                    {
                        if (item.Links.FirstOrDefault() == null)
                        {
                            foreach (var chatId in rss.chatIds)
                                RabbitMQRequest.Create(chatId.chatId, ((TextSyndicationContent)item.Content).Text);
                        }
                        else
                        {
                            foreach (var chatId in rss.chatIds)
                                RabbitMQRequest.Create(chatId.chatId, item.Links.FirstOrDefault().Uri.AbsoluteUri);
                        }

                        foreach (RssChatRelationEntity uri in RssList.Where(x => x.Key.Url == rss.url).Select(x => x.Key).ToList())
                            RssList[uri] = DateTimeOffset.Now;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Concat("Error: ", ex.Message, "\nURL: ", rss.url));
                }
            });
        }
    }
}
