using TeamBlog.Dtos;

namespace TeamBlog.Db.Access.Commands
{
    public interface IAddInsertPostNotificationCommandBuilder
    {
        AddInsertPostNotificationCommand Build(PostAddedDto postAddedDto);
    }
}