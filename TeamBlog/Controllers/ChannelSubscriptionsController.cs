using System;
using System.Web.Http;
using TeamBlog.Bl;

namespace TeamBlog.Controllers
{
    public class ChannelSubscriptionsController : ApiController
    {
        private readonly IUserFactory _userFactory;

        public ChannelSubscriptionsController(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        [HttpPost]
        [Route(@"api/subscriptions")]
        public System.Net.Http.HttpResponseMessage Subscribe([FromBody] Guid channelId)
        {
            _userFactory.GetCurrentUser().SubscribeToChannel(channelId);
            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}