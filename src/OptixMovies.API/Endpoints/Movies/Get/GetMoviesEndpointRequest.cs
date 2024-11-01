using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OptixMovies.API.Endpoints.Movies.Get;

public class GetMoviesEndpointRequest
{
    [FromQuery] public string? Title { get; set; } = string.Empty;
    [FromQuery] public string[]? Genres { get; set; } = Array.Empty<string>();

    [FromQuery] public bool? OrderByReleaseDate { get; set; } = true;
    [FromQuery] public bool? OrderByTitle { get; set; } = false;
    [FromQuery] public bool? OrderByDescending { get; set; } = true;

    [FromQuery] public int? PageSize { get; set; } = 20;
    [FromQuery] public int? PageNumber { get; set; } = 0;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
