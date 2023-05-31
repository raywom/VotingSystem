using Application.Core;
using Application.Interfaces;
using Application.VotingSystem;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Polls
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Poll Poll { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Poll).SetValidator(new ActivityValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => 
                    x.UserName == _userAccessor.GetUsername());
                //
                // var attendee = new Vote
                // {
                //     AppUser = user,
                //     Poll = request.Poll,
                //     IsHost = true
                // };
                //
                // var choices = new List<Choice>
                // {
                //     new Choice
                //     {
                //         Title = "Yes",
                //         Votes = 0
                //     },
                //     new Choice
                //     {
                //         Title = "No",
                //         Votes = 0
                //     }
                // };
                //
                // request.Poll.Voters.Add(attendee);
                request.Poll.AppUser = user;
                request.Poll.IsHost = true;
                _context.Polls.Add(request.Poll);
                
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}