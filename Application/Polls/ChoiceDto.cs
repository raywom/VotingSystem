namespace Application.Polls;

public class ChoiceDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid PollId { get; set; }
}