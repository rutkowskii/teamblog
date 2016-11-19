using Ploeh.AutoFixture;
using System.Linq;
using TeamBlog.Dtos;

namespace TeamBlog.Services
{
    public interface IUser
    {
        PostDto[] GetGeneralFeedPosts();
    }

    public class User : IUser
    {
        public PostDto[] GetGeneralFeedPosts()
        {
            return new Fixture().CreateMany<PostDto>().ToArray();
        }
    }
}