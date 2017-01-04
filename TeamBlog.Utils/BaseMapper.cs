using AutoMapper;

namespace TeamBlog.Utils
{
    public class BaseMapper<TSrc, TDest>
    {
        private static readonly IMapper DefaultMapper;

        static BaseMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSrc, TDest>();
            });

            DefaultMapper = config.CreateMapper();
        }

        public TDest Map(TSrc source)
        {
            var dest = DefaultMapper.Map<TSrc, TDest>(source);
            return dest;
        }
    }
}