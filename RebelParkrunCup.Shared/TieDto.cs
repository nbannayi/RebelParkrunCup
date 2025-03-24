using System.ComponentModel.DataAnnotations.Schema;

namespace RebelParkrunCup.Shared;
public class TieDto
{
    public int Id { get; set; }

    public int LocationId { get; set; } 

    public int Competitor1Id { get; set; } 

    public int Competitor2Id { get; set; } 

    public int Round { get; set; }    

    public string? Competitor1FirstName { get; set; }

    public string? Competitor1LastName { get; set; }

    public string? Competitor2FirstName { get; set; }

    public string? Competitor2LastName { get; set; }

    public string? Location { get; set; }

    // Time fields - Competitor 1.
    public int Competitor1ResultMins { get; set; }
    public int Competitor1ResultSecs { get; set; }
    public int Competitor1Delta { get; set; }

    // Time fields - Competitor 2.
    public int Competitor2ResultMins { get; set; }
    public int Competitor2ResultSecs { get; set; }
    public int Competitor2Delta { get; set; }

    public DateTime Date { get; set; }

    public string Competitor1FullName { get { return $"{Competitor1FirstName} {Competitor1LastName}"; } }
    public string Competitor2FullName { get { return $"{Competitor2FirstName} {Competitor2LastName}"; } }

    public string Competitor1Time { get { return $"{Competitor1ResultMins:D2}:{Competitor1ResultSecs:D2}"; } }
    public string Competitor2Time { get { return $"{Competitor2ResultMins:D2}:{Competitor2ResultSecs:D2}"; } }

    public string Competitor1DeltaTime { get { return FormatDeltaTime(Competitor1Delta); } }
    
    public string Competitor2DeltaTime { get { return FormatDeltaTime(Competitor2Delta); } }

    public int WinningCompetitor { get; set; }

    private string FormatDeltaTime(int delta)
    {
        var deltaAbsolute = Math.Abs(delta);
        var mins = deltaAbsolute / 60;
        var secs = deltaAbsolute % 60;    
        var deltaTime = $"{mins:D2}:{secs:D2}";
        return (delta < 0) ? "(" + deltaTime + ")" : deltaTime; 
    }  
}