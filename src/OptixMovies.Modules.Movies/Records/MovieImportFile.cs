namespace OptixMovies.Modules.Movies.Records;

public record MovieImportFile
{
    public string Release_Date { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public int Vote_Count { get; set; }
    public double Vote_Average { get; set; }
    public string Original_Language { get; set; }
    public string Genre { get; set; }
    public string Poster_Url { get; set; }
}
