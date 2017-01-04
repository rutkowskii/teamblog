using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Jsondtos;
using TeamBlog.Utils;

namespace TeamBlog.Controllers
{
    public class ChannelsController : ApiController
    {
        private readonly IChannelsService _channelsService;
        private readonly BaseMapper<ChannelDto, ChannelJsondto> _channelMapper;

        public ChannelsController(IChannelsService channelsService, BaseMapper<ChannelDto, ChannelJsondto> channelMapper)
        {
            _channelsService = channelsService;
            _channelMapper = channelMapper;
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
        public IEnumerable<ChannelJsondto> GetAll()
        {
            var results = _channelsService.GetAll().Select(_channelMapper.Map);
            return results;
        }
    }
}