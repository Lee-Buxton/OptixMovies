using OptixMovies.Modules.Movies.Records;

namespace OptixMovies.Modules.Movies.Services.Genre
{
    public interface IGenreService
    {
        Task<MovieGenre> CreateMovieAsync(MovieGenre movie, CancellationToken cancellationToken);
        Task<MovieGenre> DeleteMovieAsync(string id, CancellationToken cancellationToken);
        Task<MovieGenre> GetMovieGenreAsync(string id, CancellationToken cancellationToken);
        Task<List<MovieGenre>> GetMovieGenresAsync(CancellationToken cancellationToken);
        Task<List<MovieGenre>> GetMovieGenresAsync(string genreName, CancellationToken cancellationToken, bool partialMatch = false);
        Task<MovieGenre> UpdateMovieAsync(MovieGenre movie, CancellationToken cancellationToken);
    }
}