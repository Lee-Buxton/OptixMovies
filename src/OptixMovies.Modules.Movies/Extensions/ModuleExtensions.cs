using Microsoft.Extensions.DependencyInjection;
using OptixMovies.Modules.Movies.Options;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;
using OptixMovies.Modules.Movies.Services.MovieImporter;
using OptixMovies.Modules.Movies.Services.Movies;
using OptixMovies.Modules.Movies.Services.SqlQuery;

namespace OptixMovies.Modules.Movies.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddMoviesModule(this IServiceCollection services, Action<MoviesModuleOptions> configureOptions)
    {
        services.AddOptions<MoviesModuleOptions>()
            .Configure(configureOptions);

        services.AddSingleton<ICosmosService<Movie>, CosmosService<Movie>>();
        services.AddSingleton<IQueryService, QueryService>();
        services.AddSingleton<IMovieService, MovieService>();
        services.AddSingleton<IMovieImportService, MovieImportService>();

        return services;
    }
}
