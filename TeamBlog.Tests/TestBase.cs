namespace TeamBlog.Tests
{
    public class TestBase
    {
        protected TestKernel K { get; }

        public TestBase()
        {
            K = new TestKernel();
            K.Flush();
        }
    }
}