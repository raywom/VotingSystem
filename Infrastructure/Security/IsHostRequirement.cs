using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsHostRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;

            var activityId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString() ?? throw new InvalidOperationException());

            var attendee = _dbContext.Voters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AppUserId == userId && x.PollId == activityId)
                .Result;
            var poll = _dbContext.Polls
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == activityId && x.AppUserId == userId)
                .Result;

            if (attendee == null) return Task.CompletedTask;

            if (poll.IsHost) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}