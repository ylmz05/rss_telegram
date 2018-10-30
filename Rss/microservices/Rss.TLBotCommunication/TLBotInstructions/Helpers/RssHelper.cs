using System.Net;

namespace Rss.TLBotCommunication.TLBotInstructions.Helpers
{
    public class RssHelper
    {
        public static bool IsRss(string url)
        {
            try
            {
                var webRequest = WebRequest.CreateHttp(url);
                using (var response = webRequest.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
