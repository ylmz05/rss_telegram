using System.Threading;

namespace Rss.Writer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(60000);
            LifeTimeCycle.LifeTimeCycle.Create();
        }
    }
}
