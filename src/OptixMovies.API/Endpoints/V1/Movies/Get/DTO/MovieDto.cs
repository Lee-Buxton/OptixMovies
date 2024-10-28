namespace OptixMovies.API.Endpoints.V1.Movies.Get.DTO;

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

public record RatingDto
{
    public int VoteCount { get; set; }
    public double AvgScore { get; set; }
}