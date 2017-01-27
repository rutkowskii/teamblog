using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeamBlog.Jsondtos;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Mapper;
using TeamBlog.Utils;

namespace TeamBlog.Controllers
{
    public class PostsController : ApiController
    {
        private readonly PostDto2PostJsondtoMapper _postJsondtoMapper;
        private readonly IUser _currentUser;
        private readonly BaseMapper<NewPostJsondto, NewPostDto> _newpostDtoMapper;

        public PostsController(
            BaseMapper<NewPostJsondto, NewPostDto> newpostDtoMapper, 
            PostDto2PostJsondtoMapper postJsondtoMapper,
            IUser currentUser)
        {
            _postJsondtoMapper = postJsondtoMapper; 
            _currentUser = currentUser;
            _newpostDtoMapper = newpostDtoMapper;
        }

        [HttpGet]
        [Route(@"api/posts")]
        public IEnumerable<PostJsondto> GetFeedPosts()
        {
            var posts = _currentUser.GetGeneralFeedPosts();
            var postsMapped = posts.Select(_postJsondtoMapper.Map).ToArray();
            return postsMapped;
        }

        [HttpPost]
        [Route(@"api/posts")]
        public System.Net.Http.HttpResponseMessage AddNewPost([FromBody] NewPostJsondto newPost)
        {
            _currentUser.AddPost(_newpostDtoMapper.Map(newPost));

            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }

    public class UsersController : ApiController
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUsersService _usersService;

        public UsersController(ISessionProvider sessionProvider, IUsersService usersService)
        {
            _sessionProvider = sessionProvider;
            _usersService = usersService;
        }

        [HttpGet]
        [Route(@"api/currentUser")]
        public UserJsondto GetCurrentUser()
        {
            var userId = _sessionProvider.UserId;
            var user = _usersService.GetAll().First(u => u.Id == userId);
            return new UserJsondto {Name = user.Name};
        }
    }

    public class UserJsondto
    {
        public string Name { get; set; }
    }
}
