using Rss.Application.Services.Interfaces.Commands;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Messaging.AppComponents;
using System;
using System.Collections.Generic;

namespace Rss.Messaging.Exceptions
{
    public class BotKickedException
    {
        private const string BOT_KICKED = "Forbidden: bot was kicked from the channel chat";
        public BotKickedException(Exception exception, MessagingPlatform messagingPlatform)
        {
            if (exception.Message.Equals(BOT_KICKED))
            {
                IQueryRssChatRelationService queryRssChatRelationService = messagingPlatform.Resolve<IQueryRssChatRelationService>();
                ICommandRssChatRelationService commandRssChatRelationService = messagingPlatform.Resolve<ICommandRssChatRelationService>();

                Response<IList<RssChatRelationEntity>> response = queryRssChatRelationService.GetList(-1001419844651);
                foreach (var item in response.ResponseData)
                    commandRssChatRelationService.Remove(item);

                return;
            }
            //new Exception(exception);
        }
    }
}
