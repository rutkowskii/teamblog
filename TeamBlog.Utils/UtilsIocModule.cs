using TeamBlog.Utils.Datetime;

namespace TeamBlog.Utils
{
    public class UtilsIocModule : BaseIocModule
    {
        public override void Load()
        {
            this.BindTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}