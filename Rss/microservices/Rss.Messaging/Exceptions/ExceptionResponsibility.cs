using Rss.Messaging.AppComponents;
using System;

namespace Rss.Messaging.Exceptions
{
    public class ExceptionResponsibility
    {
        public ExceptionResponsibility(Exception exception, MessagingPlatform messagingPlatform)
        {
            new BotKickedException(exception, messagingPlatform);
        }
    }
}
