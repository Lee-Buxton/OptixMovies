using OptixMovies.Modules.Movies.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace OptixMovies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                    o.MaxEndpointVersion = 1;
                    o.DocumentSettings = s =>
                    {
                        s.DocumentName = "Release 1.0";
                        s.Title = "OptixMovies API";
                        s.Version = "v1.0";
                    };
                });

            builder.Services.AddMoviesModule(
                o =>
                {
                    o.Cosmos = new Modules.Movies.Options.CosmosOptions()
                    {
                        DatabaseName = builder.Configuration["Cosmos:DatabaseName"],
                        Endpoint = builder.Configuration["Cosmos:AccountEndpoint"],
                        IgnoreCertificate = builder.Configuration.GetValue<bool>("Cosmos:IgnoreCertificate"),
                        AuthKey = builder.Configuration["Cosmos:AuthKey"] ?? builder.Configuration["COSMOS_AUTHKEY"],
                        PartitionKey = builder.Configuration["Cosmos:PartitionKey"]
                    };
                    o.Query = new Modules.Movies.Options.QueryOptions()
                    {
                        AllowedFields =
                        [
                            "ReleaseDate",
                            "Title",
                            "Rating.AverageScore",
                            "Genres",
                            "OriginalLanguage"
                        ]
                    };
                });
            
            var app = builder.Build();
            app.UseFastEndpoints(c =>
            {
                c.Versioning.PrependToRoute = true;
                c.Versioning.Prefix = "v";
            })
               .UseSwaggerGen();

            app.Run();
        }
    }
}
