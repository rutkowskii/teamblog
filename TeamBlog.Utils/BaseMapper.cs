using AutoMapper;

namespace TeamBlog.Utils
{
    public class BaseMapper<TFrom, TTo>
    {
        private readonly IMapper _mapper;

        public BaseMapper()
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<TFrom, TTo>());
            _mapper = config.CreateMapper();
        }

        public TTo Map(TFrom source)
        {
            var result =_mapper.Map<TTo>(source);
            return result;
        }
    }
}
