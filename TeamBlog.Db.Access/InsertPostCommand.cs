using System;
using TeamBlog.Model;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access
{
    public class InsertPostCommand: ICommand
    {
        private readonly IMongoAdapter _mongoAdapter;
        private readonly Guid _channelId;
        private readonly string _url;
        private readonly string _description;
        private readonly Guid _userId;

        public InsertPostCommand(IMongoAdapter mongoAdapter, Guid channelId, string url, string description, Guid userId)
        {
            _mongoAdapter = mongoAdapter;
            _channelId = channelId;
            _url = url;
            _description = description;
            _userId = userId;
        }

        public void Run()
        {
            var newPostId = Guid.NewGuid();
            var insertionTime = DateTime.Now;

            var post = new Post
            {
                AuthorId = _userId,
                CreationDate = insertionTime,
                Description = _description,
                Id = newPostId,
                Url = _url
            };
            _mongoAdapter.PostCollection.InsertOne(post);
            _mongoAdapter.ChannelPostCollection.InsertOne(new ChannelPost
            {
                ChannelId = _channelId,
                InsertionTime = insertionTime,
                PostId = newPostId
            });
        }
    }
}