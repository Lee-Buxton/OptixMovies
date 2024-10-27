using OptixMovies.Modules.Movies.Records;

namespace OptixMovies.Modules.Movies.Services.Movies
{
    public interface IMovieService
    {
        Task<Movie> CreateMovieAsync(Movie movie, CancellationToken cancellationToken);
        Task<Movie> DeleteMovieAsync(string id, CancellationToken cancellationToken);
        Task<Movie> GetMovieAsync(string id, CancellationToken cancellationToken);
        Task<List<Movie>> GetMoviesAsync(Query query, CancellationToken cancellationToken);
        Task<Movie> UpdateMovieAsync(Movie movie, CancellationToken cancellationToken);
    }
}