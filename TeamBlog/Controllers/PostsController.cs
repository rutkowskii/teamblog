using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeamBlog.Jsondtos;
using TeamBlog.Jsondtos.Mapping;
using TeamBlog.Bl;

namespace TeamBlog.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IUserFactory _userFactory;
        private readonly PostJsondtoMapper _postJsondtoMapper;

        public PostsController(IUserFactory userFactory, PostJsondtoMapper postJsondtoMapper)
        {
            _userFactory = userFactory;
            _postJsondtoMapper = postJsondtoMapper;
        }

        [HttpGet]
        [Route(@"api/posts")]
        public IEnumerable<PostJsondto> GetFeedPosts()
        {
            var posts = this.CurrentUser.GetGeneralFeedPosts();
            var postsMapped = posts.Select(_postJsondtoMapper.Map).ToArray();
            return postsMapped;
        }


        [HttpPost]
        [Route(@"api/posts")]
        public System.Net.Http.HttpResponseMessage AddNewPost([FromBody] NewPostJsondto newPost)
        {
            //todo implementation. 
            // CurrentUser.AddPost();//todo mapping

            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        private IUser CurrentUser => this._userFactory.GetCurrentUser();
    }
}