using OptixMovies.API.Domain.Records;

namespace OptixMovies.API.Interfaces
{
    public interface IMovieService
    {
        (List<Movie> Movies, int TotalResults) GetMovies(string title, string[] genres, bool orderByTitle = false, bool orderByReleaseDate = true, bool orderByDescending = true, int take = 10, int skip = 0);
    }
}