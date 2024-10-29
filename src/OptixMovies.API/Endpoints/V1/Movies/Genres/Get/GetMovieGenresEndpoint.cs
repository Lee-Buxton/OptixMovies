
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Genre;

namespace OptixMovies.API.Endpoints.V1.Movies.Genres.Get;

public class GetMovieGenresEndpoint : EndpointWithoutRequest<GetMovieGenresResponse>
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly IGenreService _genreService;
    private readonly ILogger<GetMovieGenresEndpoint> _logger;
    #endregion

    #region Constructor
    public GetMovieGenresEndpoint(
        IGenreService genreService,
        ILogger<GetMovieGenresEndpoint> logger)
    {
        _genreService = genreService;
        _logger = logger;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes(
            "/Movies/Genres");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<MovieGenre> movieGenres = await _genreService.GetMovieGenresAsync(ct);

        await SendAsync(new GetMovieGenresResponse()
        {
            Genres = movieGenres
        }, 200, ct);
    }
    #endregion

    #region Private Methods

    #endregion
}
