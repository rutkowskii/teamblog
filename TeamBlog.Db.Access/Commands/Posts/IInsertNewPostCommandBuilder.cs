namespace TeamBlog.Db.Access.Commands.Posts
{
    public interface IInsertNewPostCommandBuilder
    {
        InsertNewPostCommand Build(InsertNewPostParams insertNewPostParams);
    }
}