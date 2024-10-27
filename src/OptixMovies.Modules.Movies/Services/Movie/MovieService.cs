using Microsoft.Extensions.Logging;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;

namespace OptixMovies.Modules.Movies.Services.Movies;

public class MovieService
{
    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly ICosmosService<Movie> _cosmos;
    private readonly ILogger<MovieService> _logger;
    #endregion

    #region Constructor
    public MovieService(
        ICosmosService<Movie> cosmos,
        ILogger<MovieService> logger)
    {
        _cosmos = cosmos;
        _logger = logger;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods

    #endregion

    #region Private Methods

    #endregion
}
