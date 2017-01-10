using AutoMapper;
using TeamBlog.Dtos;
using TeamBlog.Jsondtos;
using TeamBlog.Utils;

namespace TeamBlog.Mapper
{
    public class PostDto2PostJsondtoMapper : BaseMapper<PostDto, PostJsondto>
    {
        protected override void CustomConfiguration(IMappingExpression<PostDto, PostJsondto> mapping)
        {
            mapping.ForMember(dest => dest.Timestamp, 
                ops => ops.MapFrom(src => src.CreationDate.ToWebFormat()));
            mapping.ForMember(dest => dest.AddedBy,
                ops => ops.MapFrom(src => src.Author));
        }
    }
}