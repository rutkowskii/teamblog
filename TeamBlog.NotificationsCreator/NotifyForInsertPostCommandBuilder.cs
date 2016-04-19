using TeamBlog.Bus;
using TeamBlog.Db.Access;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.RedisAccess;

namespace TeamBlog.NotificationsCreator
{
    //public class NotifyForInsertPostCommandBuilder
    //{
    //    private readonly IRedisConnection _redisConnection;
    //    private readonly PostAddedUserNotificationBuilder _notificationBuilder;

    //    public NotifyForInsertPostCommandBuilder(IRedisConnection redisConnection, PostAddedUserNotificationBuilder notificationBuilder)
    //    {
    //        _redisConnection = redisConnection;
    //        _notificationBuilder = notificationBuilder;
    //    }

    //    public AddInsertPostNotificationCommand Build(PostAddedDto postAddedDto)
    //    {
    //        return new AddInsertPostNotificationCommand(_redisConnection, postAddedDto, _notificationBuilder, );
    //    }
    //}

    //todo remove it?
}