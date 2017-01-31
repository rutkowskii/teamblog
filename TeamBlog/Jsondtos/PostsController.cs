using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Mapper;
using TeamBlog.Utils;

namespace TeamBlog.Jsondtos
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
            this._postJsondtoMapper = postJsondtoMapper; 
            this._currentUser = currentUser;
            this._newpostDtoMapper = newpostDtoMapper;
        }

        [HttpGet]
        [Route(@"api/posts")]
        public IEnumerable<PostJsondto> GetFeedPosts()
        {
            var posts = this._currentUser.GetGeneralFeedPosts();
            var postsMapped = posts.Select(this._postJsondtoMapper.Map).ToArray();
            return postsMapped;
        }

        [HttpPost]
        [Route(@"api/posts")]
        public System.Net.Http.HttpResponseMessage AddNewPost([FromBody] NewPostJsondto newPost)
        {
            this._currentUser.AddPost(this._newpostDtoMapper.Map(newPost));

            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}