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
    public class QueryRssCatRelationRepo : QueryRepoBase<RssChatRelationEntity>, IQueryRssCatRelationRepo
    {
        private readonly string _connectionString;

        public QueryRssCatRelationRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public RssChatRelationEntity Get(long userId, long chatId, string url)
        {
            RssChatRelationEntity input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.ChatId, Operator.Eq, chatId));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.Url, Operator.Eq, url));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).FirstOrDefault();

            return input;
        }

        public RssChatRelationEntity Get(long userId, string name, string url)
        {
            RssChatRelationEntity input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.Name, Operator.Eq, name));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.Url, Operator.Eq, url));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).FirstOrDefault();

            return input;
        }

        public IList<RssChatRelationEntity> GetList(string AliasName)
        {
            IList<RssChatRelationEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.AliasName, Operator.Eq, AliasName));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).ToList();

            return input;
        }

        public IList<RssChatRelationEntity> GetList(long chatId)
        {
            IList<RssChatRelationEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.ChatId, Operator.Eq, chatId));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).ToList();

            return input;
        }

        public IList<RssChatRelationEntity> GetListByAliasName(long userId, string aliasName)
        {
            IList<RssChatRelationEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.AliasName, Operator.Eq, aliasName));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).ToList();

            return input;
        }

        public IList<RssChatRelationEntity> GetListByChatName(long userId, string chatName)
        {
            IList<RssChatRelationEntity> input = null;

            PredicateGroup predicateGroup = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.UserId, Operator.Eq, userId));
            predicateGroup.Predicates.Add(Predicates.Field<RssChatRelationEntity>(x => x.Name, Operator.Eq, chatName));

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                input = connection.GetList<RssChatRelationEntity>(predicateGroup).ToList();

            return input;
        }
    }
}
