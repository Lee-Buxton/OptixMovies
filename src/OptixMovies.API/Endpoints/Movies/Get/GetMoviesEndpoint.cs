using FastEndpoints;
using OptixMovies.API.Domain.Records;
using OptixMovies.API.Interfaces;

namespace OptixMovies.API.Endpoints.Movies.Get;

public class GetMoviesEndpoint : Endpoint<GetMoviesEndpointRequest, GetMoviesEndpointResponse>
{
    #region Fields
    private readonly ILogger<GetMoviesEndpoint> _logger;
    private readonly IMovieService _movieService;

    private const string _dateTimeFormat = "yyyy-MM-dd HH:mm";
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

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMoviesEndpointRequest req, CancellationToken ct)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Get Movies Endpoint - Processing Request {req.ToString()}");

        (List<Movie> Movies, int TotalResults) movies = _movieService.GetMovies(
            title: req.Title,
            genres: req.Genres,
            orderByTitle: req.OrderByTitle.HasValue ? req.OrderByTitle.Value : false,
            orderByReleaseDate: req.OrderByReleaseDate.HasValue ? req.OrderByReleaseDate.Value : true,
            orderByDescending: req.OrderByDescending.HasValue ? req.OrderByDescending.Value : true,
            take: req.PageSize.HasValue ? req.PageSize.Value : 20,
            skip: ((req.PageSize.HasValue ? req.PageSize.Value : 20) * (req.PageNumber.HasValue ? req.PageNumber.Value : 1))
            );

        await SendAsync(new GetMoviesEndpointResponse()
        {
            Movies = movies.Movies,

            PageSize = req.PageSize.HasValue ? req.PageSize.Value : 20,
            PageNumber = req.PageNumber.HasValue ? req.PageNumber.Value : 0,
            TotalResults = movies.TotalResults,
        }, 200, ct);
    }
    #endregion

    #region Private Methods

    #endregion
}
