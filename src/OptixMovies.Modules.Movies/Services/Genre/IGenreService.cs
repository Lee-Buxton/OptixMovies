using OptixMovies.Modules.Movies.Records;

namespace OptixMovies.Modules.Movies.Services.Genre
{
    public interface IGenreService
    {
        Task<MovieGenre> CreateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken);
        Task<MovieGenre> DeleteMovieGenreAsync(string id, CancellationToken cancellationToken);
        Task<MovieGenre> GetMovieGenreAsync(string id, CancellationToken cancellationToken);
        Task<List<MovieGenre>> GetMovieGenresAsync(CancellationToken cancellationToken);
        Task<MovieGenre> UpdateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken);
        Task<Guid> FindOrCreateMovieGenreAsync(string movieGenreName, CancellationToken cancellationToken);
        Task<string> MapIdtoNameAsync(Guid id, CancellationToken cancellationToken);
        Guid MapNameToId(string genreName, CancellationToken cancellationToken);
        Task<Guid> MapNameToIdAsync(string genreName, CancellationToken cancellationToken);
    }
}