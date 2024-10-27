namespace OptixMovies.Modules.Movies.Options;

public class QueryOptions
{
    public string[] AllowedFields { get; set; } =
    [
        "releaseDate",
        "title",
        "rating.averageScore",
        "genres",
        "originalLanguage"
    ];
}
