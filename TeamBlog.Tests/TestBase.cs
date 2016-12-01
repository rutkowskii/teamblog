namespace TeamBlog.Tests
{
    public class TestBase
    {
        protected TestKernel K { get; }

        public TestBase()
        {
            this.K = new TestKernel();
            this.K.Flush();
        }
    }
}