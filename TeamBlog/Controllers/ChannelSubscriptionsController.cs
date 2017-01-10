using System;
using System.Web.Http;
using TeamBlog.Bl;

namespace TeamBlog.Controllers
{
    public class ChannelSubscriptionsController : ApiController
    {
        private readonly IUser _currentUser;

        public ChannelSubscriptionsController(IUser currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpPost]
        [Route(@"api/subscriptions")]
        public System.Net.Http.HttpResponseMessage Subscribe([FromBody] Guid channelId)
        {
            _currentUser.SubscribeToChannel(channelId);
            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}