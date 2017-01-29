using TeamBlog.Services.Sessions;
using TeamBlog.Utils;

namespace TeamBlog.Bl
{
    public class BlIocModule : BaseIocModule
    {
        public override void Load()
        {
           BindTransient<IUser, CurrentUser>();
           BindTransient<IChannelsService, ChannelsService>();
           BindTransient<IUsersService, UsersService>();
           BindTransient<ISessionProvider, SessionProvider>();
           BindSingleton<FakeSessionContainer>();
        }
    }
}