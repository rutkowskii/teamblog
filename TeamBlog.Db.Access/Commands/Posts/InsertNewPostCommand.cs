using System;
using System.Linq;
using TeamBlog.Model;
using TeamBlog.MongoAccess;
using TeamBlog.Utils;
using TeamBlog.Utils.Datetime;

namespace TeamBlog.Db.Access.Commands.Posts
{
    public class InsertNewPostCommand: ICommand<InsertNewPostCommandResult>
    {
        private readonly IMongoAdapter _mongoAdapter;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly InsertNewPostParams _newPostParams;

        public InsertNewPostCommand(
            IMongoAdapter mongoAdapter, 
            IDateTimeProvider dateTimeProvider, 
            InsertNewPostParams newPostParams)
        {
            _mongoAdapter = mongoAdapter;
            _dateTimeProvider = dateTimeProvider;
            _newPostParams = newPostParams;
        }

        public InsertNewPostCommandResult Run()
        {
            var newPostId = Guid.NewGuid();
            var insertionTime = _dateTimeProvider.UtcNow;

            var post = new Post
            {
                AuthorId = _newPostParams.UserId,
                CreationDate = insertionTime,
                Content = _newPostParams.Content,
                Id = newPostId,
                Title = _newPostParams.Title
            };

            _mongoAdapter.PostCollection.InsertOne(post);
            _mongoAdapter.ChannelPostCollection.InsertMany(_newPostParams.ChannelIds
                .Select(channelId =>
                    new ChannelPost
                    {
                        ChannelId = channelId,
                        InsertionTime = insertionTime,
                        PostId = newPostId
                    }
            ));

            return new InsertNewPostCommandResult
            {
                NewPostId = newPostId,
                InsertionTime = insertionTime
            };
        }
    }
}