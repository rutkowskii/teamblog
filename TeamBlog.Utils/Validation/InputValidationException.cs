using System;

namespace TeamBlog.Utils.Validation
{
    public class InputValidationException : Exception
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}