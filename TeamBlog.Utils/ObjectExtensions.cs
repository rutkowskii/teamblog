namespace TeamBlog.Utils
{
    public static class ObjectExtensions
    {
        public static T[] AsArray<T>(this T obj)
        {
            return new T[] {obj};
        }
    }
}