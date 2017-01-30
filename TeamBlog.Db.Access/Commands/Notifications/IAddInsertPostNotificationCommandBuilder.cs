namespace TeamBlog.Db.Access.Commands.Notifications
{
    public interface IAddInsertPostNotificationCommandBuilder
    {
        AddInsertPostNotificationCommand Build(AddInsertPostNotificationCommandParams postAdded);
    }
}