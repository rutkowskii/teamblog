using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries
{
    //todo move to utils
    public class PagingParams
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }

    public class GetUserNotificationsQuery
    {
        private IDatabase _redisDb;
        private PagingParams _pagingParams;
        private Guid _userId;

        public GetUserNotificationsQuery(IDatabase redisDb, PagingParams pagingParams, Guid userId1)
        {
            _redisDb = redisDb;
            _pagingParams = pagingParams;
            _userId = userId1;
        }

        public IEnumerable<PostAddedUserNotification> Run()
        {
            var userNotificationsKey = RedisDbObjects.UserNotificationsKey(_userId);
            //todo common redis dlogic for paging
            //todo we get the max score then -10?? and pass it to the redis?????
            _redisDb.SortedSetRangeByScore()
        } 
    }

    public class GetUserNotificationsQueryBuilder
    {
    }

}
