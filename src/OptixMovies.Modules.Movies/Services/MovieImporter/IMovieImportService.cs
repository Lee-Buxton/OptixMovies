
namespace OptixMovies.Modules.Movies.Services.MovieImporter
{
    public interface IMovieImportService
    {
        Task ImportFromCsvAsync(byte[] csvFile, CancellationToken cancellationToken);
    }
}