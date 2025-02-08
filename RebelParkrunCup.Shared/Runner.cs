namespace RebelParkrunCup.Shared;

public class Runner
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }        
    public int BaselineTimeMins { get; set; }  // Stores the baseline time minute portion.
    public int BaselineTimeSecs { get; set; }  // Stores the baseline time seconds portion.
}