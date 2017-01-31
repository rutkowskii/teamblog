namespace TeamBlog.Utils
{
    public class PagingParams
    {
        private const int TakeAllIndicator = -1;
        public int Index { get; set; }
        public int Count { get; set; }

        public static PagingParams All => new PagingParams {Count = TakeAllIndicator, Index = TakeAllIndicator};

        public bool TakesAll => Count == TakeAllIndicator && Index == TakeAllIndicator;
    }
}