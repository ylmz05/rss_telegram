using Rss.Reader.AppComponents;
using Rss.Reader.Reader;
using System;
using System.Threading;

namespace Rss.Reader.LifeTimeCycle
{
    public class LifeTimeCycle
    {

        public static void Create(ReaderPlatform readerPlatform)
        {

            try
            {
                ReaderList.Load(readerPlatform);

                while (true)
                {
                    Thread.Sleep(60000);
                    ReaderList.SyncList(readerPlatform);
                    ReaderList.GetLatestNews();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Create(readerPlatform);
            }
        }
    }
}
