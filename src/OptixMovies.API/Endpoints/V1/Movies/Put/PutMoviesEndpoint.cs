using OptixMovies.API.Endpoints.V1.Movies.Post;
using OptixMovies.Modules.Movies.Services.Movies;

namespace OptixMovies.API.Endpoints.V1.Movies.Put;

public class PutMoviesEndpoint : Endpoint<PutMoviesEndpointRequest, PutMoviesEndpointResponse>
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly IMovieService _movieService;
    private readonly ILogger<PostMoviesEndpoint> _logger;
    #endregion

    #region Constructor
    public PutMoviesEndpoint(
        IMovieService movieService,
        ILogger<PostMoviesEndpoint> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }
    #endregion

    #region Public Methods
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/movies");
        AllowAnonymous();
        Version(1);
    }

    public override Task HandleAsync(PostMoviesEndpointRequest req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods

    #endregion
}

