using Microsoft.Extensions.Logging;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;
using OptixMovies.Modules.Movies.Services.SqlQuery;
using System.Runtime.CompilerServices;

namespace OptixMovies.Modules.Movies.Services.Movies;

public class MovieService : IMovieService
{
    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly ICosmosService<Movie> _cosmos;
    private readonly ILogger<MovieService> _logger;
    private readonly IQueryService _query;
    #endregion

    #region Constructor
    public MovieService(
        ICosmosService<Movie> cosmos,
        IQueryService query,
        ILogger<MovieService> logger)
    {
        _cosmos = cosmos;
        _logger = logger;
        _query = query;
    }
    #endregion

    #region Public Methods
    public async Task<Movie> GetMovieAsync(string id, CancellationToken cancellationToken)
    {
        return await _cosmos.GetItemAsync(id, cancellationToken);
    }

    public async Task<List<Movie>> GetMoviesAsync(Query query, CancellationToken cancellationToken)
    {
        return await _cosmos.GetItemsAsync(cancellationToken);
    }

    public async Task<Movie> CreateMovieAsync(Movie movie, CancellationToken cancellationToken)
    {
        return await _cosmos.CreateItemAsync(movie, cancellationToken);
    }

    public async Task<Movie> UpdateMovieAsync(Movie movie, CancellationToken cancellationToken)
    {
        return await _cosmos.UpdateItemAsync(movie, cancellationToken);
    }

    public async Task<Movie> DeleteMovieAsync(string id, CancellationToken cancellationToken)
    {
        Movie movie = await _cosmos.GetItemAsync(id, cancellationToken);
        await _cosmos.DeleteItemAsync(id, cancellationToken);
        return movie;
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods

    #endregion
}