namespace OptixMovies.API.Domain.Records;

public record Movie
{
    public DateOnly ReleaseDate { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public int VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string OriginalLanguage { get; set; }
    public string[] Genres { get; set; }
    public string Poster_Url { get; set; }
}