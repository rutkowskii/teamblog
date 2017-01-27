using System;
using System.Linq;

namespace TeamBlog.Utils.Validation
{
    public abstract class BasicValidator<T> : IValidator<T>
    {
        protected abstract ValidationRule<T>[] Rules { get; }

        public ValidationResult IsValid(T input)
        {
            var errors = Rules
                .Select(rule => rule.GetError(input))
                .Where(error => error != null)
                .ToArray();
            return errors.Any() ? new ValidationResult(errors) : ValidationResult.Success;
        }

        public void ThrowIfInvalid(T input)
        {
            var validationResult = IsValid(input);
            if (!validationResult.IsSuccess)
            {
                throw new InputValidationException(string.Join(Environment.NewLine, validationResult.Errors));
            }
        }
    }
}