namespace RebelParkrunCup.Shared;

public class Runner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;        
    public List<BaselineTime> BaselineTimes { get; set; } = new();
}