namespace RebelParkrunCup.Shared;
public class Competitor
{
    public int Id { get; set; }

    // Foreign Key - Runner.
    public int RunnerId { get; set; }
    
    public Runner Runner { get; set; } = null!;

    // Foreign Key - Tournament.
    public int TournamentId { get; set; }

    public Tournament Tournament { get; set; } = null!;

    // Time fields.
    public int BaselineTimeMins { get; set; }
    public int BaselineTimeSecs { get; set; }
    
    public List<Tie> TiesAsCompetitor1 { get; set; } = new();
    public List<Tie> TiesAsCompetitor2 { get; set; } = new();
}