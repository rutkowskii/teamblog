namespace TeamBlog.Utils.Validation
{
    interface IValidator<T>
    {
        ValidationResult IsValid(T input);
    }
}