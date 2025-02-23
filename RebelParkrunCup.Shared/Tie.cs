using System.ComponentModel.DataAnnotations.Schema;

namespace RebelParkrunCup.Shared;
public class Tie
{
    public int Id { get; set; }

    // Foreign Key - Tournament.    
    public int TournamentId { get; set; }   
    public Tournament Tournament { get; set; }

    public int Round { get; set; }    

    // Foreign Key - Competitor1.
    public int Competitor1Id { get; set; }
    public Competitor Competitor1 { get; set; }

    // Foreign Key - Competitor2.
    public int Competitor2Id { get; set; }
    public Competitor Competitor2 { get; set; }

    // Foreign Key - location.
    public int LocationId { get; set; }
    public Location Location { get; set; }

    // Time fields - Competitor 1.
    public int Competitor1ResultMins { get; set; }
    public int Competitor1ResultSecs { get; set; }
    public int Competitor1Delta { get; set; }

    // Time fields - Competitor 2.
    public int Competitor2ResultMins { get; set; }
    public int Competitor2ResultSecs { get; set; }
    public int Competitor2Delta { get; set; }

    public DateTime Date { get; set; }

    // Competitor 1 win.
    [NotMapped]
    public bool Competitor1Win => Competitor1Delta < Competitor2Delta;
}