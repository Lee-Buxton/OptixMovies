using Microsoft.Extensions.DependencyInjection;
using OptixMovies.Modules.Movies.Options;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;

namespace OptixMovies.Modules.Movies.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddMoviesModule(this IServiceCollection services, Action<MoviesOptions> configureOptions)
    {
        services.AddOptions<MoviesOptions>()
            .Configure(configureOptions);

        services.AddSingleton<ICosmosService<Movie>, CosmosService<Movie>>();

        return services;
    }
}
