using ScrapySharp.Network;
using System;

namespace Rss.Writer.WebScrapping.Base
{
    public abstract class BaseScrapping
    {
        public string ScrapUri(string uri, string focusedNode)
        {
            try
            {
                return (new ScrapingBrowser()).NavigateToPage(new Uri(uri)).Html.SelectSingleNode(focusedNode).InnerText.Trim();
            }
            catch (Exception)
            {
                Console.WriteLine("error while scrapping text.");
                return null;
            }
        }

        public abstract string GetLatestUpdate();
    }
}
