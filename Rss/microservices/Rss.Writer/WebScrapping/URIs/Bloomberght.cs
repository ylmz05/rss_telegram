using Rss.Writer.WebScrapping.Base;

namespace Rss.Writer.WebScrapping.URIs
{
    public class Bloomberght : BaseScrapping
    {
        private const string URL = "http://www.bloomberght.com/sondakika";
        private const string SCRAPPING_PATH = "/html[1]/body[1]/div[1]/div[5]/div[4]/div[2]/div[1]/div[1]/div[1]/span[1]/span[2]";

        public override string GetLatestUpdate()
        {
           return ScrapUri(URL, SCRAPPING_PATH);
        }
    }
}
