using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Polls
{
    public class Cancel
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Polls
                    .Include(a => a.Voters).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken: cancellationToken);

                if (user == null) return null;
                
                activity.IsCancelled = !activity.IsCancelled;

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem cancelling poll");
            }
        }
    }
}