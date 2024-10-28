using OptixMovies.Modules.Movies.Extensions;

namespace OptixMovies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddFastEndpoints();
            builder.Services.AddMoviesModule(
                o =>
                {
                    o.Cosmos = new Modules.Movies.Options.CosmosOptions()
                    {
                        DatabaseName = "Movies",
                        ConnectionString = ""
                    };
                });

            var app = builder.Build();
            app.UseFastEndpoints();
            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
