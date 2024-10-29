using Newtonsoft.Json;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;

namespace OptixMovies.Modules.Movies.Records;

public record Movie : ICosmosItem
{
    [JsonProperty("id")]
    public Guid Id { get; init; }

    public DateOnly ReleaseDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double TMDBPopularity { get; set; }
    public Rating Rating { get; set; }
    public string OriginalLanguage { get; set; }
    public List<string> Genres { get; set; }
    public string PosterURL { get; set; }

}
