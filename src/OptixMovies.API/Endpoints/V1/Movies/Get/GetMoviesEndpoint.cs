using OptixMovies.API.DTO;
using OptixMovies.Modules.Movies.Records;
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
        int resultCount = 0;
        List<MovieDto> moviesDto = new List<MovieDto>();

        if (req.Id != string.Empty)
        {
            Movie movie = await _movieService.GetMovieAsync(req.Id, ct);
            moviesDto.Add(
                await MapMovieToMovieDto(movie, ct));

            resultCount = 1;
        }
        else
        {
            Query query = new Query()
            {
                Filter = req.Filter,
                OrderBy = req.OrderBy,
                Top = req.Top.Value,
                Skip = req.Skip.Value
            };

            List<Movie> movies = await _movieService.GetMoviesAsync(
                query, 
                ct);


            foreach (Movie movie in movies)
            {
                moviesDto.Add(
                    await MapMovieToMovieDto(movie, ct));
            }

            resultCount = await _movieService.GetMoviesCountAsync(
                query, 
                ct);
        }

        await SendAsync(new GetMoviesEndpointResponse()
        {
            Movies = moviesDto,
            PageSize = req.Top.Value,
            Page = CalculatePageNumber(req.Top.Value, req.Skip.Value),
            ResultsCount = resultCount
        });
    }
    #endregion

    #region Private Methods
    private async Task<MovieDto> MapMovieToMovieDto(Movie movie, CancellationToken cancellationToken)
    {
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
                AverageScore = movie.Rating.AverageScore,
                VoteCount = movie.Rating.VoteCount
            },
            Genres = movie.Genres,
            PosterUrl = movie.PosterURL,
        };
    }

    private int CalculatePageNumber(int top, int skip)
    {
        if (skip == 0)
            return 0;

        int page = top / skip;
        if (top % skip != 0)
        {
            page++;
        }
        return page;
    }
    #endregion
}