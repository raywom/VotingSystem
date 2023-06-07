namespace Domain;

public class Choice
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid PollId { get; set; }
    public Poll Poll { get; set; }
    public ICollection<Vote> Voters { get; set; } = new List<Vote>();
}