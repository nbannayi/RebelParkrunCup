namespace RebelParkrunCup.Shared;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; }
    public List<Competitor> Competitors { get; set; } = new();
    public List<Tie> Ties { get; set; } = new();
}