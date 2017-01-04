using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeamBlog.Jsondtos;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Utils;

namespace TeamBlog.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IUserFactory _userFactory;
        private readonly BaseMapper<PostDto, PostJsondto> _postJsondtoMapper;
        private readonly BaseMapper<NewPostJsondto, NewPostDto> _newpostDtoMapper;

        public PostsController(
            IUserFactory userFactory, 
            BaseMapper<NewPostJsondto, NewPostDto> newpostDtoMapper, 
            BaseMapper<PostDto, PostJsondto> postJsondtoMapper)
        {
            _userFactory = userFactory;
            _postJsondtoMapper = postJsondtoMapper;
            _newpostDtoMapper = newpostDtoMapper;
        }

        [HttpGet]
        [Route(@"api/posts")]
        public IEnumerable<PostJsondto> GetFeedPosts()
        {
            var posts = CurrentUser.GetGeneralFeedPosts();
            var postsMapped = posts.Select(_postJsondtoMapper.Map).ToArray();
            return postsMapped;
        }

        [HttpPost]
        [Route(@"api/posts")]
        public System.Net.Http.HttpResponseMessage AddNewPost([FromBody] NewPostJsondto newPost)
        {
            CurrentUser.AddPost(_newpostDtoMapper.Map(newPost));

            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        private IUser CurrentUser => _userFactory.GetCurrentUser();
    }
}
