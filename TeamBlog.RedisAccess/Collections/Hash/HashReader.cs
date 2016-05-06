using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBlog.RedisAccess.Collections.Hash
{
    public class HashReader : RedisAccessorBase
    {
        private readonly string _hashIdentifier;

        public HashReader(IRedisConnection redisConnection, string hashIdentifier) : base(redisConnection)
        {
            _hashIdentifier = hashIdentifier;
        }


    }
}