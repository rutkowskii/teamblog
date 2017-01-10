using System.Linq;
using AutoMapper;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Jsondtos;
using TeamBlog.Utils;
using TeamBlog.Utils.Datetime;

namespace TeamBlog.Mapper
{
    public class PostDto2PostJsondtoMapper : BaseMapper<PostDto, PostJsondto>
    {
        private readonly IUsersService _usersService;
        private readonly IChannelsService _channelsService;

        public PostDto2PostJsondtoMapper(IUsersService usersService, IChannelsService channelsService)
        {
            _usersService = usersService;
            _channelsService = channelsService;
        }

        protected override void CustomConfiguration(IMappingExpression<PostDto, PostJsondto> mapping)
        {
            mapping.ForMember(dest => dest.Timestamp, 
                ops => ops.MapFrom(src => src.CreationDate.ToWebFormat()));
            mapping.ForMember(dest => dest.AddedBy, ops => ops.MapFrom(p => ResolveAuthor(p)));
            mapping.ForMember(dest => dest.Channels, ops => ops.MapFrom(p => ResolveChannels(p)));
        }

        private string[] ResolveChannels(PostDto postDto) //todo perf. 
        {
            return  postDto.Channels
                .Select(id => _channelsService.GetAll().First(ch => ch.Id == id).Name)
                .ToArray();
        }

        private string ResolveAuthor(PostDto post)
        {
            return _usersService.GetAll().First(u => u.Id == post.Author).Name;
        }
    }
}