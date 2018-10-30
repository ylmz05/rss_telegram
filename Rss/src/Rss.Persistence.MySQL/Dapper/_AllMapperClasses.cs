using DapperExtensions.Mapper;
using Rss.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rss.Persistence.MySQL.Dapper
{
    public class BotEntityMapper : AutoClassMapper<BotEntity> { }
    public class ChannelEntityMapper : AutoClassMapper<ChannelEntity> { }
    public class RssEntityMapper : AutoClassMapper<RssEntity> { }
    public class UserEntityMapper : AutoClassMapper<UserEntity> { }
}
