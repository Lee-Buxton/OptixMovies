using System.ComponentModel.DataAnnotations;

namespace OptixMovies.Modules.Movies.Options;

public class CosmosOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseName { get; set; }
}
