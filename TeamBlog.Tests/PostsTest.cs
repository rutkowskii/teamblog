﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TeamBlog.Bl;
using TeamBlog.Controllers;
using TeamBlog.Jsondtos;
using TeamBlog.Utils;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostsTest : TestBase
    {
        private IEnumerable<PostJsondto> _actualPosts;
        private const string ChannelName = "smieszki-channel";

        [Scenario]
        public void AddThenRetrievePosts()
        {
            "GIVEN the channel is created".x(() => { InsertChannel(); });

            "and GIVEN the user is subscribed to the channel".x(() =>
            {
                K.Resolve<ChannelSubscriptionsController>().Subscribe(ChannelId);
            });

            "and GIVEN the posts are inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa");
                InsertPost(title: "xyz", content: "zzz");
            });

            "WHEN querying for the user feed".x(() =>
            {
                _actualPosts = K.Resolve<PostsController>()
                    .GetFeedPosts();
            });

            "THEN proper posts should be returned".x(() =>
            {
                _actualPosts
                    .Select(p => p.Content)
                    .ShouldBeEquivalentTo(new[] {"zzz", "aaa"});
            });
        }

        private void InsertChannel()
        {
            K.Resolve<ChannelsController>().AddNewChannel(new NewChannelJsondto { Name = ChannelName});
        }

        private Guid ChannelId => 
            K.Resolve<ChannelsController>().GetAll()
                .Where(ch => ch.Name == ChannelName)
                .Select(ch => ch.Id)
                .First();

        private void InsertPost(string title, string content)
        {
            var postsController = K.Resolve<PostsController>();
            postsController.AddNewPost(new NewPostJsondto
            {
                Channels = ChannelId.AsArray(),
                Content = content,
                Title = title
            });
        }
    }
}