using System.Linq;
using Ploeh.AutoFixture;
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