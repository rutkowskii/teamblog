using System;
using System.Collections.Generic;

namespace TeamBlog.Db.Access
{
    public interface IQuery<T>
    {
        T[] Run();
    }
}