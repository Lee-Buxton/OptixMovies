using Microsoft.Extensions.DependencyInjection;
using OptixMovies.Modules.Movies.Options;

namespace OptixMovies.Modules.Movies.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddMoviesModule(this IServiceCollection services, Action<MoviesOptions> configureOptions)
    {
        services.AddOptions<MoviesOptions>()
            .Configure(configureOptions);

        return services;
    }
}
