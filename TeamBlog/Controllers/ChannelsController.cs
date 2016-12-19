using System.Collections.Generic;
using System.Web.Http;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Jsondtos;

namespace TeamBlog.Controllers
{
    public class ChannelsController : ApiController
    {
        private readonly IChannelsService _channelsService;

        public ChannelsController(IChannelsService channelsService)
        {
            _channelsService = channelsService;
        }

        [HttpPost]
        [Route(@"api/channels")]
        public System.Net.Http.HttpResponseMessage AddNewChannel([FromBody] NewChannelJsondto newChannel)
        {
            _channelsService.AddChannel(newChannel.Name);
            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Route(@"api/channels")]
        public IEnumerable<ChannelDto> GetAll()
        {
            return _channelsService.GetAll();
        }
    }
}