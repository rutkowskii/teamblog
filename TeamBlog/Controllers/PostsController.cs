using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Mvc;
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route(@"api/posts")]
        public IEnumerable<PostJsondto> GetFeedPosts()
        {
            var posts = _currentUser.GetGeneralFeedPosts();
            var postsMapped = posts.Select(_postJsondtoMapper.Map).ToArray();
            return postsMapped;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route(@"api/posts")]
        public System.Net.Http.HttpResponseMessage AddNewPost([FromBody] NewPostJsondto newPost)
        {
            _currentUser.AddPost(_newpostDtoMapper.Map(newPost));

            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }

    public class DummyController : Controller
    {
        [System.Web.Mvc.Route(@"api/dummy")]
        [System.Web.Mvc.Authorize]
        public ActionResult About()
        {
            return View((User as ClaimsPrincipal).Claims);
        }
    }
}
