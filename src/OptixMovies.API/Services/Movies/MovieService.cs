using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using OptixMovies.API.Domain.Records;
using OptixMovies.API.Interfaces;
using OptixMovies.API.Services.Movies.Options;
using System.Globalization;

namespace OptixMovies.API.Services.Movies;

public class MovieService : IMovieService
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private const string _dateTimeFormat = "yyyy-MM-dd HH:mm";


    private readonly List<Movie> _movies = new List<Movie>();
    private readonly ILogger<MovieService> _logger;
    private readonly MovieServiceOptions _options;


    private int _movieCount = 0;
    #endregion

    #region Constructor
    public MovieService(
        ILogger<MovieService> logger,
        IOptions<MovieServiceOptions> options)
    {
        _logger = logger;
        _options = options.Value;

        InitialiseMovieService();
    }
    #endregion

    #region Public Methods
    public (List<Movie> Movies, int TotalResults) GetMovies(
        string title,
        string[] genres,
        bool orderByTitle = false,
        bool orderByReleaseDate = true,
        bool orderByDescending = true,
        int take = 20,
        int skip = 0)
    {

        title = title ?? string.Empty;
        genres = genres ?? Array.Empty<string>();

        List<Movie> movies = _movies.Where(movie =>
                                            movie.Title.Contains(title) &&
                                            genres.All(genre => movie.Genres.Contains(genre, StringComparer.InvariantCultureIgnoreCase)))
                                    .ToList();

        int totalResults = movies.Count;

        if (orderByTitle)
        {
            if (orderByDescending)
                movies = movies.OrderByDescending(movie => movie.Title).ToList();
            else
                movies = movies.OrderBy(movie => movie.Title).ToList();
        }
        else if (orderByReleaseDate)
        {
            if (orderByDescending)
                movies = movies.OrderByDescending(movie => movie.ReleaseDate).ToList();
            else
                movies = movies.OrderBy(movie => movie.ReleaseDate).ToList();
        }

        return (Movies: movies.Skip(skip).Take(take).ToList(), TotalResults: totalResults);

    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods
    private void InitialiseMovieService()
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Intialising");

        try
        {
            using (StreamReader reader = new StreamReader(_options.MovieDbFile))
            using (CsvReader csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\n"
            }))
            {
                List<dynamic> csvRecords = csv.GetRecords<dynamic>().ToList();

                foreach (dynamic csvRecord in csvRecords)
                {
                    _movies.Add(
                        MapCsvRecordToMovie(csvRecord));
                    _movieCount++;
                }
            }
            _logger.LogInformation($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Movies Loaded: {_movieCount}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Error importing movie DB");
            _logger.LogError($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Movie DB File: {_options.MovieDbFile}");
            _logger.LogError(ex, $"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Exception Details");

            throw;
        }

        _logger.LogInformation($"{DateTime.UtcNow.ToString(_dateTimeFormat)} - Movie Service - Intialisation Complete.");
    }

    private Movie MapCsvRecordToMovie(dynamic csvRecord)
    {
        Movie movie = new Movie()
        {
            ReleaseDate = DateOnly.Parse(csvRecord.Release_Date) ?? DateTime.UtcNow,
            Title = csvRecord.Title ?? string.Empty,
            Overview = csvRecord.Overview ?? string.Empty,
            Popularity = Convert.ToDouble(csvRecord.Popularity) ?? 0,
            VoteCount = Convert.ToInt16(csvRecord.Vote_Count) ?? 0,
            VoteAverage = Convert.ToDouble(csvRecord.Vote_Average) ?? 0,
            OriginalLanguage = csvRecord.Original_Language ?? string.Empty,
            Genres = ((string)csvRecord.Genre).Split(',', StringSplitOptions.TrimEntries) ?? Array.Empty<string>(),
            Poster_Url = csvRecord.Poster_Url ?? string.Empty
        };

        return movie;
    }
    #endregion
}
