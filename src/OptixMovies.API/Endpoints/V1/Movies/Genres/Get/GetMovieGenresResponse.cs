using OptixMovies.Modules.Movies.Records;
using System.Net;

namespace OptixMovies.API.Endpoints.V1.Movies.Genres.Get;

public class GetMovieGenresResponse
{
    public List<MovieGenre> Genres { get; set; }
}
