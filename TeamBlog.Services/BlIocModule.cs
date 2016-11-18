using TeamBlog.Utils;

namespace TeamBlog.Services
{
    public class BlIocModule : BaseIocModule
    {
        public override void Load()
        {
           BindTransient<IUserFactory, UserFactory>();
        }
    }
}