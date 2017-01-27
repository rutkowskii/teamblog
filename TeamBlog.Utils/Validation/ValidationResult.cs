using System.Linq;

namespace TeamBlog.Utils.Validation
{
    public class ValidationResult
    {
        public string[] Errors { get; }
        public bool IsSuccess => !Errors.Any();

        public ValidationResult(params string [] errors)
        {
            Errors = errors.ToArray();
        }

        public static ValidationResult Success
        {
            get {  return new ValidationResult();}
        }
    }
}