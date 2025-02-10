namespace RebelParkrunCup.Shared;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool InProgress { get; set; }
    public List<BaselineTime> BaselineTimes { get; set; } = new();
}