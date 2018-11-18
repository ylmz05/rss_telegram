using DapperExtensions;
using MySql.Data.MySqlClient;
using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Query;
using Rss.Persistence.Repos.Interfaces.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rss.Persistence.MySQL.Repos.Classes.Queries
{
    public class QueryRssRepo : QueryRepoBase<RssEntity>, IQueryRssRepo
    {
        private readonly string _connectionString;

        public QueryRssRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public RssEntity Get(long userId, string aliasName)
        {
            RssEntity input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<RssEntity>(x => x.AliasName, Operator.Eq, aliasName));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssEntity>(predicateGroup).FirstOrDefault();

            return input;
        }
        public IList<RssEntity> GetList(long userId)
        {
            IList<RssEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssEntity>(x => x.UserId, Operator.Eq, userId));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssEntity>(predicateGroup).ToList();

            return input;
        }
    }
}
