using System;

namespace TeamBlog.Services.Sessions
{
    public class FakeSessionContainer
    {
        private readonly Guid _jamesDoeId = new Guid("388FED2C-879F-4F9E-86A9-68C4C410F56E");
        private readonly Guid _markSmithId = new Guid("4FAF742B-53FD-45B6-99AB-5C0FEABB070A");

        public Guid UserId { get; private set; }

        public void SetupDefaultSession()
        {
            UserId = _jamesDoeId;
        }

        public void Toggle()
        {
            UserId = UserId == _jamesDoeId ? _markSmithId : _jamesDoeId;
        }
    }
}