using Domain;
using FluentValidation;

namespace Application.VotingSystem
{
    public class ActivityValidator : AbstractValidator<Poll>
    {
        public ActivityValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CloseDate).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}