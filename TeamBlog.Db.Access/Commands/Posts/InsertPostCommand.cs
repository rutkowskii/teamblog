using System;
using System.Linq;
using TeamBlog.Model;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class InsertNewPostCommand: ICommand<InsertNewPostCommandResult>
    {
        private readonly IMongoAdapter _mongoAdapter;
        private readonly Guid[] _channelIds;
        private readonly string _title;
        private readonly string _content;
        private readonly Guid _userId;

        public InsertNewPostCommand(IMongoAdapter mongoAdapter, Guid[] channelIds, string title, string content, Guid userId)
        {
            _mongoAdapter = mongoAdapter;
            _channelIds = channelIds;
            this._title = title;
            this._content = content;
            _userId = userId;
        }

        public InsertNewPostCommandResult Run()
        {
            var newPostId = Guid.NewGuid();
            var insertionTime = DateTime.Now;

            var post = new Post
            {
                AuthorId = _userId,
                CreationDate = insertionTime,
                Content = _content,
                Id = newPostId,
                Title = _title
            };
            _mongoAdapter.PostCollection.InsertOne(post);
            _mongoAdapter.ChannelPostCollection.InsertMany(_channelIds.Select(channelId =>
                new ChannelPost
                {
                    ChannelId = channelId,
                    InsertionTime = insertionTime,
                    PostId = newPostId
                }
            ));

            //todo not setting our Url
            return new InsertNewPostCommandResult
            {
                NewPostId = newPostId
            };
        }
    }
}