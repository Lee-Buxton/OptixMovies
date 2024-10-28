using OptixMovies.Modules.Movies.Services.Movies;

namespace OptixMovies.API.Endpoints.V1.Movies.Get;

public class GetMoviesEndpoint : Endpoint<GetMoviesEndpointRequest, GetMoviesEndpointResponse>
{
    

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly ILogger<GetMoviesEndpoint> _logger;
    private readonly IMovieService _movieService;
    #endregion

    #region Constructor
    public GetMoviesEndpoint(
        ILogger<GetMoviesEndpoint> logger,
        IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes(
            "/Movies",
            "/Movies/{Id}");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(GetMoviesEndpointRequest req, CancellationToken ct)
    {
        if (req.Id != string.Empty)
        {
            // Retrieve the the movie that matches the ID. 
        }
        else
        {
            // Return a list of movies, that match the filter.
        }

        return;
    }

    #endregion

    #region Private Methods

    #endregion
}
