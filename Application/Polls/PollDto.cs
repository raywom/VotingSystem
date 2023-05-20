using Application.Comments;
using Application.Profiles;

namespace Application.VotingSystem;

public class PollDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime CloseDate { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string HostUsername { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<VoterDto> Voters { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
}