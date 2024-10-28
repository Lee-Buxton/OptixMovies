using OptixMovies.Modules.Movies.Services.Movies;

namespace OptixMovies.API.Endpoints.V1.Movies.Post;

public class PostMoviesEndpoint : Endpoint<PostMoviesEndpointRequest, PostMoviesEndpointResponse>
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
    public PostMoviesEndpoint(
        IMovieService movieService,
        ILogger<PostMoviesEndpoint> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/movies/{Id}");
        AllowAnonymous();
        Version(1);
    }

    public override Task HandleAsync(PostMoviesEndpointRequest req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
    #endregion

    #region Private Methods

    #endregion
}
