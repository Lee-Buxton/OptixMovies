using Microsoft.Extensions.Logging;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;
using OptixMovies.Modules.Movies.Services.Movies;
using System.Web;

namespace OptixMovies.Modules.Movies.Services.Genre;

public class GenreService : IGenreService
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly ICosmosService<MovieGenre> _cosmos;
    private readonly ILogger<MovieService> _logger;
    #endregion

    #region Constructor
    public GenreService(
        ICosmosService<MovieGenre> cosmos,
        ILogger<MovieService> logger)
    {
        _cosmos = cosmos;
        _logger = logger;
    }
    #endregion

    #region Public Methods
    public async Task<MovieGenre> GetMovieGenreAsync(string id, CancellationToken cancellationToken)
    {
        return await _cosmos.GetItemAsync(id, cancellationToken);
    }

    public async Task<List<MovieGenre>> GetMovieGenresAsync(CancellationToken cancellationToken)
    {
        return await GetMovieGenresAsync(string.Empty, cancellationToken);
    }

    public async Task<List<MovieGenre>> GetMovieGenresAsync(string genreName, CancellationToken cancellationToken, bool partialMatch = false)
    {
        return await _cosmos.GetItemsAsync(cancellationToken);
    }

    public async Task<MovieGenre> CreateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken)
    {
        return await _cosmos.CreateItemAsync(movieGenre, cancellationToken);
    }

    public async Task<MovieGenre> UpdateMovieGenreAsync(MovieGenre movieGenre, CancellationToken cancellationToken)
    {
        return await _cosmos.UpdateItemAsync(movieGenre, cancellationToken);
    }

    public async Task<MovieGenre> DeleteMovieGenreAsync(string id, CancellationToken cancellationToken)
    {
        MovieGenre movieGenre = await _cosmos.GetItemAsync(id, cancellationToken);
        await _cosmos.DeleteItemAsync(id, cancellationToken);
        return movieGenre;
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods

    #endregion
}
