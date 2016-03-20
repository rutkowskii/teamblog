using TeamBlog.Utils;

namespace TeamBlog.MongoAccess
{
    public class MongoAccessIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IMongoDbProvider, TmpMongoDbProvider>();
            BindTransient<IMongoAdapter, MongoAdapter>();
        }
    }
}