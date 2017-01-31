using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Jsondtos;
using TeamBlog.Model;
using TeamBlog.Services.Sessions;
using TeamBlog.Utils;

namespace TeamBlog.Controllers
{
    public class UserNotificationsController : ApiController
    {
        private readonly GetUserNotificationsQueryBuilder _userNotificationsQueryBuilder;
        private readonly ISessionProvider _sessionProvider;
        private readonly BaseMapper<PostAddedUserNotification, UserNotificationJsondto> _notificationsMapper;

        public UserNotificationsController(GetUserNotificationsQueryBuilder userNotificationsQueryBuilder,
            ISessionProvider sessionProvider,
            BaseMapper<PostAddedUserNotification, UserNotificationJsondto> notificationsMapper)
        {
            _userNotificationsQueryBuilder = userNotificationsQueryBuilder;
            _sessionProvider = sessionProvider;
            _notificationsMapper = notificationsMapper;
        }

        [HttpGet]
        [Route(@"api/userNotifications")]
        public IEnumerable<UserNotificationJsondto> GetAll()
        {
            var query = _userNotificationsQueryBuilder.Build(_sessionProvider.UserId);
            return RunQuery(query);
        }

        [HttpGet]
        [Route(@"api/userNotifications")]
        public IEnumerable<UserNotificationJsondto> Get([FromBody] PagingParams pagingParams)
        {
            var query = _userNotificationsQueryBuilder.Build(pagingParams, _sessionProvider.UserId);
            return RunQuery(query);
        }

        private IEnumerable<UserNotificationJsondto> RunQuery(GetUserNotificationsQuery query)
        {
            var notifications = query.Run();
            var jsonDtos = notifications.Select(_notificationsMapper.Map).ToArray();
            return jsonDtos;
        }
    }
}