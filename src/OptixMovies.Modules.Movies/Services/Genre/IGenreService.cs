using OptixMovies.Modules.Movies.Records;

namespace OptixMovies.Modules.Movies.Services.Genre
{
    public interface IGenreService
    {
        Task<MovieGenre> CreateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken);
        Task<MovieGenre> DeleteMovieGenreAsync(string id, CancellationToken cancellationToken);
        Task<MovieGenre> GetMovieGenreAsync(string id, CancellationToken cancellationToken);
        Task<List<MovieGenre>> GetMovieGenresAsync(CancellationToken cancellationToken);
        Task<List<MovieGenre>> GetMovieGenresAsync(string genreName, CancellationToken cancellationToken, bool partialMatch = false);
        Task<MovieGenre> UpdateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken);
        Task<Guid> FindOrCreateMovieGenreAsync(string movieGenreName, CancellationToken cancellationToken);
    }
}