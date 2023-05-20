using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ListPolls
    {
        public class Query : IRequest<Result<List<UserPollDto>>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<UserPollDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<UserPollDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Voters
                    .Where(u => u.AppUser.UserName == request.Username)
                    .OrderBy(a => a.Poll.CloseDate)
                    .ProjectTo<UserPollDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();
                
                var today = DateTime.UtcNow;

                query = request.Predicate switch
                {
                    "past" => query.Where(a => a.Date <= today),
                    "hosting" => query.Where(a => a.HostUsername == request.Username),
                    _ => query.Where(a => a.Date >= today)
                };

                var activities = await query.ToListAsync();

                return Result<List<UserPollDto>>.Success(activities);
            }
        }
    }
}