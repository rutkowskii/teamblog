using TeamBlog.Utils;

namespace TeamBlog.Bl
{
    public class BlIocModule : BaseIocModule
    {
        public override void Load()
        {
           BindTransient<IUserFactory, UserFactory>();
        }
    }
}