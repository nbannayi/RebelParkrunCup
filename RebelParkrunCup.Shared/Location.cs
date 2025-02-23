namespace RebelParkrunCup.Shared;

public class Location
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Tie> Ties { get; set; } = new();
}