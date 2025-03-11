using System.ComponentModel.DataAnnotations.Schema;

namespace RebelParkrunCup.Shared;
public class TieDto
{
    public int Id { get; set; }

    public int Round { get; set; }    

    public string Competitor1FirstName { get; set; }

    public string Competitor1LastName { get; set; }

    public string Competitor2FirstName { get; set; }

    public string Competitor2LastName { get; set; }

    public string Location { get; set; }

    // Time fields - Competitor 1.
    public int Competitor1ResultMins { get; set; }
    public int Competitor1ResultSecs { get; set; }
    public int Competitor1Delta { get; set; }

    // Time fields - Competitor 2.
    public int Competitor2ResultMins { get; set; }
    public int Competitor2ResultSecs { get; set; }
    public int Competitor2Delta { get; set; }

    public DateTime Date { get; set; }

    public bool Competitor1Win => Competitor1Delta < Competitor2Delta;
    public string Competitor1FullName { get { return $"{Competitor1FirstName} {Competitor1LastName}"; } }
    public string Competitor2FullName { get { return $"{Competitor2FirstName} {Competitor2LastName}"; } }

    public string Competitor1Time { get { return $"{Competitor1ResultMins:D2}:{Competitor1ResultSecs:D2}"; } }
    public string Competitor2Time { get { return $"{Competitor1ResultMins:D2}:{Competitor1ResultSecs:D2}"; } }
}