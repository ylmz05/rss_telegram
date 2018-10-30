using DapperExtensions;
using MySql.Data.MySqlClient;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Query;
using Rss.Persistence.Repos.Interfaces.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rss.Persistence.MySQL.Repos.Classes.Queries
{
    public class QueryChannelRepo : QueryRepoBase<ChannelEntity>, IQueryChannelRepo
    {
        private readonly string _connectionString;

        public QueryChannelRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public ChannelEntity Get(long userId, long chatId)
        {
            ChannelEntity input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<ChannelEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<ChannelEntity>(x => x.ChatId, Operator.Eq, chatId));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<ChannelEntity>(predicateGroup).FirstOrDefault();

            return input;
        }
        public ChannelEntity Get(long userId, string name)
        {
            ChannelEntity input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<ChannelEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<ChannelEntity>(x => x.Name, Operator.Eq, name));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<ChannelEntity>(predicateGroup).FirstOrDefault();

            return input;
        }
        public IList<ChannelEntity> GetList(int type)
        {
            IList<ChannelEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<ChannelEntity>(x => x.Type, Operator.Eq, type));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<ChannelEntity>(predicateGroup).ToList();

            return input;
        }
    }
}
