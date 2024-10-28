using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using OptixMovies.Modules.Movies.Records;
using OptixMovies.Modules.Movies.Services.Genre;
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
    private readonly IGenreService _genreService;
    private readonly IMovieService _movieService;
    private readonly ILogger<MovieImportService> _logger;

    private int _importCount;
    #endregion

    #region Constructor
    public MovieImportService(
        IGenreService genreService,
        IMovieService movieService,
        ILogger<MovieImportService> logger)
    {
        _genreService = genreService;
        _movieService = movieService;
        _logger = logger;
    }
    #endregion

    #region Public Methods
    public async Task ImportFromCsvAsync(byte[] csvFile, CancellationToken cancellationToken)
    {
        List<MovieImportFile> movieImportFiles;

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
            await ProcessMovieAsync(item, cancellationToken);
        }
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
            Genres = new List<Guid>()
        };

        foreach (var genre in movieImportFile.Genre.Split(new string[] {","}, StringSplitOptions.TrimEntries))
        {
            movie.Genres.Add(
                await _genreService.FindOrCreateMovieGenreAsync(genre, cancellationToken));
        }

        await _movieService.CreateMovieAsync(movie, cancellationToken);

        _importCount++;
    }
    #endregion
}
