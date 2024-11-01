using FastEndpoints;
using FastEndpoints.Swagger;
using OptixMovies.API.Interfaces;
using OptixMovies.API.Services.Movies;
using OptixMovies.API.Services.Movies.Options;

namespace OptixMovies.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOptions<MovieServiceOptions>()
            .Configure(o =>
            {
                o.MovieDbFile = builder.Configuration["MovieService:MovieDbFile"];
            });

        builder.Services.AddFastEndpoints().SwaggerDocument();

        builder.Services.AddSingleton<IMovieService, MovieService>();

        var app = builder.Build();
        app.UseFastEndpoints().UseSwaggerGen();

        app.Run();
    }
}
