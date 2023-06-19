using Application.Core;
using Application.Interfaces;
using Application.VotingSystem;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Polls
{
    public class Details
    {
        public class Query : IRequest<Result<PollDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PollDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PollDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Polls
                    .ProjectTo<PollDto>(_mapper.ConfigurationProvider, new{currentUsername = _userAccessor.GetUsername()})
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<PollDto>.Success(activity);
            }
        }
    }
}
