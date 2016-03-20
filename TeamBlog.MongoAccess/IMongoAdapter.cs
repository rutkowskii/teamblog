using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TeamBlog.Model;

namespace TeamBlog.MongoAccess
{
    public interface IMongoAdapter
    {
        IMongoCollection<Post> PostCollection { get; }
        IMongoCollection<Channel> ChannelCollection { get; }
        IMongoCollection<ChannelPost> ChannelPostCollection { get; }
    }
}
