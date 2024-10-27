namespace OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;

public interface ICosmosService<T>
{
    Task<T> GetItemAsync(string id, CancellationToken cancellationToken, string containerName = null);
    Task<List<T>> GetItemsAsync(CancellationToken cancellationToken, string sqlQuery = null, string containerName = null);
    Task<T> CreateItemAsync(T item, CancellationToken cancellationToken, string containerName = null);
    Task<T> UpdateItemAsync(T item, CancellationToken cancellationToken, string containerName = null);
    Task DeleteItemAsync(string id, CancellationToken cancellationToken, string containerName = null);
    Task<int> GetCountAsync(string sqlWhere, string containerName, CancellationToken cancellationToken);
}