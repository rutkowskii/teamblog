using System.Linq;
using MongoDB.Driver;
using TeamBlog.Dtos;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries.Channels
{
    public class ChannelsQuery : IQuery<ChannelDto>
    {
        private readonly IMongoAdapter _mongoAdapter;

        public ChannelsQuery(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public ChannelDto[] Run()
        {
            var channels = _mongoAdapter.ChannelCollection.AsQueryable().ToArray();
            //todo mapping 
            return channels
                .Select(ch => new ChannelDto {Id = ch.Id, Name = ch.Name})
                .ToArray();
        }
    }
}
