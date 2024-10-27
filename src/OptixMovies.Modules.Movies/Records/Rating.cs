namespace OptixMovies.Modules.Movies.Records;

public record Rating
{
    public double AverageScore { get; set; }
    public int VoteCount { get; set; }
}
