using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Logging;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Movies;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;

namespace OptixMovies.Modules.Movies.Services.MovieImporter;

public class MovieImportService : IMovieImportService
{
    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly IMovieService _movieService;
    private readonly ILogger<MovieImportService> _logger;

    private int _importCount;
    #endregion

    #region Constructor
    public MovieImportService(
        IMovieService movieService,
        ILogger<MovieImportService> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }
    #endregion

    #region Public Methods
    public async Task<int> ImportFromCsvAsync(byte[] csvFile, CancellationToken cancellationToken)
    {
        List<MovieImportFile> movieImportFiles;
        List<Movie> movieList = new List<Movie>();

        using (MemoryStream memoryStream = new MemoryStream(csvFile))
        using (StreamReader reader = new StreamReader(memoryStream))
        using (CsvReader csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) 
        { 
            NewLine = "\n"
        }))
        {
            movieImportFiles = csv.GetRecords<MovieImportFile>().ToList();
        }

        foreach (var item in movieImportFiles)
        {
            //await ProcessMovieAsync(item, cancellationToken);
            movieList.Add(
                await MapImportedMoviesToMovie(item, cancellationToken));
        }

        _movieService.BulkCreateMovieAsync(movieList, cancellationToken);

        return movieList.Count();
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods
    private async Task ProcessMovieAsync(MovieImportFile movieImportFile, CancellationToken cancellationToken)
    {
        Movie movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = movieImportFile.Title,
            Description = movieImportFile.Overview,
            ReleaseDate = DateOnly.Parse(movieImportFile.Release_Date),
            TMDBPopularity = movieImportFile.Popularity,
            OriginalLanguage = movieImportFile.Original_Language,
            Rating = new Rating()
            {
                VoteCount = movieImportFile.Vote_Count,
                AverageScore = movieImportFile.Vote_Average
            },
            PosterURL = movieImportFile.Poster_Url,
            Genres = movieImportFile.Genre.ToLower().Split(new string[] { "," }, StringSplitOptions.TrimEntries).ToList()
        };

        await _movieService.CreateMovieAsync(movie, cancellationToken);

        _importCount++;
    }

    private async Task<Movie> MapImportedMoviesToMovie(MovieImportFile movieImportFile, CancellationToken cancellationToken)
    {
        Movie movie = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = movieImportFile.Title,
            Description = movieImportFile.Overview,
            ReleaseDate = DateOnly.Parse(movieImportFile.Release_Date),
            TMDBPopularity = movieImportFile.Popularity,
            OriginalLanguage = movieImportFile.Original_Language,
            Rating = new Rating()
            {
                VoteCount = movieImportFile.Vote_Count,
                AverageScore = movieImportFile.Vote_Average
            },
            PosterURL = movieImportFile.Poster_Url,
            Genres = movieImportFile.Genre.ToLower().Split(new string[] { "," }, StringSplitOptions.TrimEntries).ToList()
        };
        
        return movie; 
    }
    #endregion
}
