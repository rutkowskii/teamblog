using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    public interface IChannelsService
    {
        void AddChannel(string name);
        ChannelDto[] GetAll();
    }
}