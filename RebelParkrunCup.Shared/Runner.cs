namespace RebelParkrunCup.Shared;

public class Runner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ParkrunID { get; set; } = string.Empty;
    public List<Competitor> Competitors { get; set; } = new();
}