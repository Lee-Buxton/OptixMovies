using OptixMovies.API.DTO;

namespace OptixMovies.API.Endpoints.V1.Movies.Get;

public class GetMoviesEndpointResponse
{

    public List<MovieDto> Movies { get; set; } = new List<MovieDto>();
    
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public int ResultsCount { get; set; }
}
