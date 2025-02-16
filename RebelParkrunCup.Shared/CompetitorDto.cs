namespace RebelParkrunCup.Shared;

public class CompetitorDto
{
    public int Id { get; set; }
    
    public int RunnerId { get; set; }

    public string RunnerFirstName { get; set; }
    
    public string RunnerLastName { get; set; }

    public string RunnerParkrunId { get; set; }

    // Time fields.
    public int BaselineTimeMins { get; set; }
    public int BaselineTimeSecs { get; set; }
}