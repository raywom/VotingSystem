using Application.Core;
using Domain;

namespace Application.Polls
{
    public class PollParams : PagingParams
    {
        public Choice Choice { get; set; }
        public bool IsHost { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }
}
