using System;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Channels;
using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    class ChannelsService : IChannelsService
    {
        private readonly ICreateChannelCommandBuilder _createChannelCommandBuilder;
        private readonly IChannelsQueryBuilder _channelsQueryBuilder;

        public ChannelsService(ICreateChannelCommandBuilder createChannelCommandBuilder, 
            IChannelsQueryBuilder channelsQueryBuilder)
        {
            _createChannelCommandBuilder = createChannelCommandBuilder;
            _channelsQueryBuilder = channelsQueryBuilder;
        }

        public void AddChannel(string name)
        {
            _createChannelCommandBuilder.Build(name).Run();
        }

        public ChannelDto[] GetAll()
        {
            return _channelsQueryBuilder.Build().Run();
        }
    }

    class UsersService : IUsersService
    {
        private GetUsersQuery _getUsersQuery;

        public UsersService(GetUsersQuery getUsersQuery)
        {
            _getUsersQuery = getUsersQuery;
        }

        public UserDto[] GetAll()
        {
            return _getUsersQuery.Run();
        }
    }

    public interface IUsersService
    {
        UserDto[] GetAll();
    }
}
