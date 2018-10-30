using ScrapySharp.Network;
using System;

namespace Rss.Writer.WebScrapping.Base
{
    public abstract class BaseScrapping
    {
        public string ScrapUri(string uri, string focusedNode)
        {
            return (new ScrapingBrowser()).NavigateToPage(new Uri(uri)).Html.SelectSingleNode(focusedNode).InnerText.Trim();
        }

        public abstract string GetLatestUpdate();
    }
}
