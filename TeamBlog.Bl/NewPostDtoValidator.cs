using System.Linq;
using TeamBlog.Dtos;
using TeamBlog.Utils.Validation;

namespace TeamBlog.Bl
{
    class NewPostDtoValidator : BasicValidator<NewPostDto>
    {
        protected override ValidationRule<NewPostDto>[] Rules => new[]
        {
            new ValidationRule<NewPostDto>(p => p.Channels.Any(), "Post must belong to at least one channel"),
            new ValidationRule<NewPostDto>(p => p.Content != null, "Post must have content"),
            new ValidationRule<NewPostDto>(p => p.Title != null, "Post must have title"),
        };
    }
}