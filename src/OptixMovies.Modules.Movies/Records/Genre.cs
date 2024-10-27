using Newtonsoft.Json;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;

namespace OptixMovies.Modules.Movies.Records;

public record MovieGenre : ICosmosItem
{
    [JsonProperty("id")]
    public Guid Id { get; init; }
    public string Name { get; set; }
}
