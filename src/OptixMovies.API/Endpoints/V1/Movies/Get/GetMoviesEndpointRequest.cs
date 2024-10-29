using FastEndpoints;

namespace OptixMovies.API.Endpoints.V1.Movies.Get;

public record GetMoviesEndpointRequest
{
    [QueryParam]
    public string? Id { get; set; } = string.Empty;
    public string? Filter { get; set; } = string.Empty;
    public string? OrderBy { get; set; } = string.Empty;
    public int? Top { get; set; } = 10;
    public int? Skip { get; set; } = 0;
}
