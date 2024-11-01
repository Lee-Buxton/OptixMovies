using OptixMovies.API.Domain.Records;

namespace OptixMovies.API.Endpoints.Movies.Get;

public class GetMoviesEndpointResponse
{
    public List<Movie> Movies { get; set; } = new List<Movie>();

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalResults { get; set; }
}
