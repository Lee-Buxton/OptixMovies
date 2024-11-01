using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OptixMovies.API.Endpoints.Movies.Get;

public class GetMoviesEndpointRequest
{
    private bool _orderByReleaseDate = true;
    private bool _orderByTitle = false;

    [FromQuery] public string? Title { get; set; } = string.Empty;
    [FromQuery] public string[]? Genres { get; set; } = Array.Empty<string>();

    [FromQuery] public bool? OrderByReleaseDate 
    { 
        get
        {
            return _orderByReleaseDate;
        }
        set
        {
            if (value.Value)
            {
                _orderByReleaseDate = value.Value;
                _orderByTitle = false;
            }
            else
            {
                _orderByReleaseDate = value.Value;
                _orderByTitle = true;
            }
        }
    }
    [FromQuery] public bool? OrderByTitle
    {
        get
        {
            return _orderByTitle;
        }
        set
        {
            if (value.Value)
            {
                _orderByReleaseDate = false;
                _orderByTitle = value.Value;
            }
            else
            {
                _orderByReleaseDate = true;
                _orderByTitle = value.Value;
            }
            
        }
    }
    [FromQuery] public bool? OrderByDescending { get; set; } = true;

    [FromQuery] public int? PageSize { get; set; } = 20;
    [FromQuery] public int? PageNumber { get; set; } = 0;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
