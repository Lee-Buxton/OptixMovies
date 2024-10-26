namespace OptixMovies.API.Endpoints.V1.Movies.Get;

public class GetMoviesEndpointResponse
{

    public List<object> Movies { get; set; } = new List<object>();
    
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int ResultsCount { get; set; }
}
