using System.Linq;
using System.Web.Http;
using TeamBlog.Bl;
using TeamBlog.Jsondtos;
using TeamBlog.Services.Sessions;

namespace TeamBlog.Controllers
{
    public class UsersController : ApiController
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUsersService _usersService;

        public UsersController(ISessionProvider sessionProvider, IUsersService usersService)
        {
            _sessionProvider = sessionProvider;
            _usersService = usersService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route(@"api/currentUser")]
        public UserJsondto GetCurrentUser()
        {
            var userId = _sessionProvider.UserId;
            var user = _usersService.GetAll().First(u => u.Id == userId);
            return new UserJsondto {Name = user.Name};
        }
    }

    public class DebuggingController : ApiController
    {
        private readonly FakeSessionContainer _sessionContainer;

        public DebuggingController(FakeSessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route(@"api/toggle")]
        public System.Net.Http.HttpResponseMessage Toggle()
        {
            _sessionContainer.Toggle();
            return new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}