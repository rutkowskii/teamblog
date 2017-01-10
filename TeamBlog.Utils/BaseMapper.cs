using AutoMapper;

namespace TeamBlog.Utils
{
    public class BaseMapper<TSrc, TDest>
    {
        private static MapperConfiguration MapperConfiguration;

        public TDest Map(TSrc source)
        {
            InitializeConfigurationIfNeeded();
            var dest = MapperConfiguration.CreateMapper().Map<TSrc, TDest>(source);
            return dest;
        }

        private void InitializeConfigurationIfNeeded()
        {
            if (MapperConfiguration != null) return;
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                var mapping = cfg.CreateMap<TSrc, TDest>();
                CustomConfiguration(mapping);
            });
        }

        protected virtual void CustomConfiguration(IMappingExpression<TSrc, TDest> mapping)
        {
            
        }
    }
}