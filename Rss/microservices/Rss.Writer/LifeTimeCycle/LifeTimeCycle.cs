using Rss.Writer.Rss;
using Rss.Writer.WebScrapping.Base;
using Rss.Writer.WebScrapping.URIs;
using System;
using System.Threading;

namespace Rss.Writer.LifeTimeCycle
{
    public class LifeTimeCycle
    {
        public static void Create()
        {
            BaseScrapping bloomberght = new Bloomberght();

            try
            {
                while (true)
                {
                    Thread.Sleep(3000);

                    string latestNews = bloomberght.GetLatestUpdate();
                    if (RssFeedHelper.IsContentExists(latestNews) && !string.IsNullOrEmpty(latestNews))
                        RssFeedHelper.Save("ty-scrap-news", latestNews);

                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Create();
            }
        }
    }
}
