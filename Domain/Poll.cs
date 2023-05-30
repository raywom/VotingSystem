namespace Domain;

public class Poll
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreateDate { get; set ;} = DateTime.UtcNow;
    public DateTime CloseDate { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsHost { get; set; }
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
    public ICollection<Vote> Voters { get; set; } = new List<Vote>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}