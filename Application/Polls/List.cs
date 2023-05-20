using Application.Core;
using Application.Interfaces;
using Application.VotingSystem;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Polls
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PollDto>>>
        {
            public PollParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PollDto>>>
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

            public async Task<Result<PagedList<PollDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Polls
                    .Where(x => x.CloseDate >= request.Params.StartDate)
                    .OrderBy(d => d.CloseDate)
                    .ProjectTo<PollDto>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                if (request.Params.Choice != null && !request.Params.IsHost)
                {
                    query = query.Where(x => x.Voters.Any(a => a.Username == _userAccessor.GetUsername()));
                }

                if (request.Params.IsHost && request.Params.Choice == null)
                {
                    query = query.Where(x => x.HostUsername == _userAccessor.GetUsername());
                }

                return Result<PagedList<PollDto>>
                    .Success(await PagedList<PollDto>.CreateAsync(query,
                        request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}
