namespace OptixMovies.API.DTO;

public class MovieDto
{
    public Guid Id { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Popularity { get; set; }
    public string OriginalLanguage { get; set; }
    public RatingDto Rating { get; set; }
    public List<string> Genres { get; set; }
    public string PosterUrl { get; set; }
}
