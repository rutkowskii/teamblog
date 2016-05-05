namespace TeamBlog.Utils
{
    public class PagingParams
    {
        public int Index { get; set; }
        public int Count { get; set; }

        public static PagingParams All
        {
            get {  return new PagingParams { Count = -1, Index = -1};}
        }
    }
}