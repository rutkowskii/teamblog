namespace TeamBlog.Utils
{
    public class PagingParams
    {
        private const int TakeAllIndicator = -1;
        public int Index { get; set; }
        public int Count { get; set; }

        public static PagingParams All
        {
            get { return new PagingParams {Count = TakeAllIndicator, Index = TakeAllIndicator}; }
        }

        public bool TakesAll
        {
            get { return Count == TakeAllIndicator && Index == TakeAllIndicator; }
        }
    }
}