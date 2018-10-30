using Rss.TLBotCommunication.UserSession;
using System.Collections.Generic;
using System.Linq;

namespace Rss.TLBotCommunication.TLBotInstructions.Helpers
{
    public class SessionHelper
    {
        private static List<Session> Sessions { get; set; }

        static SessionHelper()
        {
            Sessions = new List<Session>();
        }

        public static void AddSession(Session session)
        {
            if (Sessions.Where(x => x.UserId == session.UserId).FirstOrDefault() == null)
               Sessions.Add(session);
        }
        public static void RemoveSession(long userId)
        {
            if (Sessions.Where(x => x.UserId == userId).FirstOrDefault() != null)
                Sessions.Remove(Sessions.Where(x => x.UserId == userId).FirstOrDefault());
        }
        public static Session IsCodeExist(string code)
        {
            return Sessions.Where(x => x.Code == code).FirstOrDefault();
        }
        
        public static Session GetSession(long userId)
        {
            return Sessions.Where(x => x.UserId == userId).FirstOrDefault();
        }
    }
}
