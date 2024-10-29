using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using OptixMovies.Modules.Movies.Services.MovieImporter;
using OptixMovies.Modules.Movies.Services.Movies;
using System.Security.Cryptography.Xml;

namespace OptixMovies.API.Endpoints.V1.Movies.Import.Put;

public class PutMoviesImportEndpoint : Endpoint<PutMoviesImportRequest, PutMoviesImportResponse>
{
    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly IMovieImportService _movieImportService;
    private readonly ILogger<PutMoviesImportEndpoint> _logger;
    #endregion

    #region Constructor
    public PutMoviesImportEndpoint(
        IMovieImportService movieImportService,
        ILogger<PutMoviesImportEndpoint> logger)
    {
        _movieImportService = movieImportService;
        _logger = logger;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/movies/import");
        AllowFileUploads();
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(PutMoviesImportRequest req, CancellationToken ct)
    {
        int importCount;
        using (MemoryStream ms = new MemoryStream()) 
        {
            await req.File.CopyToAsync(ms, ct);

            importCount = await _movieImportService.ImportFromCsvAsync(
                ms.ToArray(),
                ct
            );
        }

        await SendAsync(new PutMoviesImportResponse()
        {
            MoviesImported = importCount
        }, 200, ct);
    }
    #endregion

    #region Private Methods

    #endregion
}
