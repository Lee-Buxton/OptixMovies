
namespace OptixMovies.Modules.Movies.Services.MovieImporter
{
    public interface IMovieImportService
    {
        Task<int> ImportFromCsvAsync(byte[] csvFile, CancellationToken cancellationToken);
    }
}