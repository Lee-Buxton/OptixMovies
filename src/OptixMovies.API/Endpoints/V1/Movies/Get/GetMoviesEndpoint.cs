using OptixMovies.API.Endpoints.V1.Movies.Get.DTO;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Genre;
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
    private readonly IGenreService _genreService;
    #endregion

    #region Constructor
    public GetMoviesEndpoint(
        ILogger<GetMoviesEndpoint> logger,
        IMovieService movieService,
        IGenreService genreService)
    {
        _logger = logger;
        _movieService = movieService;
        _genreService = genreService;
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
        List<MovieDto> moviesDto = new List<MovieDto>();

        if (req.Id != string.Empty)
        {
            Movie movie = await _movieService.GetMovieAsync(req.Id, ct);
            moviesDto.Add(
                await MapMovieToMovieDto(movie, ct));
        }
        else
        {
            List<Movie> movies = await _movieService.GetMoviesAsync(new Query()
            {
                Filter = req.Filter,
                OrderBy = req.OrderBy,
                Top = req.Top,
                Skip = req.Skip
            }, ct);

            foreach (Movie movie in movies)
            {
                moviesDto.Add(
                    await MapMovieToMovieDto(movie, ct));
            }
        }

        await SendAsync(new GetMoviesEndpointResponse()
        {
            Movies = moviesDto,
            PageSize = req.Top,
            Page = req.Top % req.Skip,
            ResultsCount = 0
        });
    }
    #endregion

    #region Private Methods
    private async Task<MovieDto> MapMovieToMovieDto(Movie movie, CancellationToken cancellationToken)
    {
        List<string> genres = new List<string>();

        foreach (var genre in movie.Genres)
        {
            genres.Add(
                await _genreService.MapIdtoNameAsync(genre, cancellationToken));
        }

        return new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            OriginalLanguage = movie.OriginalLanguage,
            Popularity = movie.TMDBPopularity,
            Rating = new RatingDto()
            {
                AvgScore = movie.Rating.AverageScore,
                VoteCount = movie.Rating.VoteCount
            },
            Genres = genres,
            PosterUrl = movie.PosterURL,
        };
    }
    #endregion
}