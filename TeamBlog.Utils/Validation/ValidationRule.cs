using System;

namespace TeamBlog.Utils.Validation
{
    public class ValidationRule<T>
    {
        private readonly Func<T, bool> _whenIsValid;
        private readonly string _errorMessage;

        public ValidationRule(Func<T, bool> whenIsValid, string errorMessage)
        {
            _errorMessage = errorMessage;
            _whenIsValid = whenIsValid;
        }

        public string GetError(T input)
        {
            return _whenIsValid(input) ? null : _errorMessage;
        }
    }
}