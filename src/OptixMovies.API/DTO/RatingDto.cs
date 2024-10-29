namespace OptixMovies.API.DTO;

public record RatingDto
{
    public int VoteCount { get; set; }
    public double AverageScore { get; set; }
}