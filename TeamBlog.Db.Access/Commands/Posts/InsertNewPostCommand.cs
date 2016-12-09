using System;
using System.Linq;
using TeamBlog.Model;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands.Posts
{
    public class InsertNewPostCommand: ICommand<InsertNewPostCommandResult>
    {
        private readonly IMongoAdapter _mongoAdapter;
        private readonly InsertNewPostParams _newPostParams;

        public InsertNewPostCommand(IMongoAdapter mongoAdapter, InsertNewPostParams newPostParams)
        {
            _mongoAdapter = mongoAdapter;
            _newPostParams = newPostParams;
        }

        public InsertNewPostCommandResult Run()
        {
            var newPostId = Guid.NewGuid();
            var insertionTime = DateTime.Now;

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
                NewPostId = newPostId
            };
        }
    }
}