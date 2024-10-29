using Humanizer;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptixMovies.Modules.Movies.Options;
using OptixMovies.Modules.Movies.Services.Azure.Cosmos.Interfaces;
using System.Net;

namespace OptixMovies.Modules.Movies.Services.Azure.Cosmos;

public class CosmosService<T> : ICosmosService<T> where T : ICosmosItem
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly Database _cosmos;
    private readonly ILogger _logger;
    private readonly CosmosOptions _options;
    #endregion

    #region Constructor
    public CosmosService(
        IOptions<MoviesModuleOptions> options,
        ILogger<CosmosService<T>> logger)
    {
        _logger = logger;
        _options = options.Value.Cosmos;

        _cosmos = InitializeCosmosService();
    }
    #endregion

    #region Public Methods
    public async Task<T> GetItemAsync(string id, CancellationToken cancellationToken, string containerName = null)
    {
        try
        {
            var container = await GetContainerAsync(containerName);
            var account = await container.ReadItemAsync<T>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return account.Resource;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving item from Cosmos");
            throw;
        }
    }

    public async Task<List<T>> GetItemsAsync(CancellationToken cancellationToken, string sqlQuery = null, string containerName = null)
    {
        try
        {
            var container = await GetContainerAsync(containerName);
            var query = container.GetItemQueryIterator<T>(queryText: sqlQuery);
            var items = new List<T>();
            while (query.HasMoreResults)
            {
                var response = query.ReadNextAsync(cancellationToken);
                items.AddRange(response.Result.ToList());
            }
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving items from Cosmos");
            throw;
        }
    }

    public async Task<T> CreateItemAsync(T item, CancellationToken cancellationToken, string containerName = null)
    {
        try
        {
            var container = await GetContainerAsync(containerName);
            var response = await container.CreateItemAsync(item, new PartitionKey(item.Id.ToString()), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating item in Cosmos");
            throw;
        }
    }

    public async Task<T> UpdateItemAsync(T item, CancellationToken cancellationToken, string containerName = null)
    {
        try
        {
            var container = await GetContainerAsync(containerName);
            var response = await container.UpsertItemAsync(item, new PartitionKey(item.Id.ToString()), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item in Cosmos");
            throw;
        }
    }

    public async Task DeleteItemAsync(string id, CancellationToken cancellationToken, string containerName = null)
    {
        try
        {
            var container = await GetContainerAsync(containerName);
            ItemResponse<T> itemResponse = await container.DeleteItemAsync<T>(id, new PartitionKey(id), cancellationToken: cancellationToken);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Item with id: {id} not found in Cosmos", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item from Cosmos");
            throw;
        }
    }

    public async Task<int> GetCountAsync(string sqlWhere, CancellationToken cancellationToken, string containerName = null)
    {
        try
        {
            string strQuery = "";
            var container = await GetContainerAsync(containerName);

            if (!string.IsNullOrEmpty(sqlWhere))
                strQuery = string.Format("SELECT VALUE COUNT(1) FROM c {0}", sqlWhere);
            else
                strQuery = string.Format("SELECT VALUE COUNT(1) FROM c");

            QueryDefinition query = new QueryDefinition(strQuery);
            FeedIterator<int> queryResultSetIterator = container.GetItemQueryIterator<int>(query);
            List<int> results = new List<int>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<int> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (int item in currentResultSet)
                {
                    results.Add(item);
                }
            }

            return results.First();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving counts from Cosmos");
            throw;
        }
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods
    private Database InitializeCosmosService()
    {

        CosmosClient cosmosClient;

        if (_options.IgnoreCertificate)
        {
            CosmosClientOptions options = new()
            {
                HttpClientFactory = () => new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }),
                ConnectionMode = ConnectionMode.Gateway,
            };

            cosmosClient = new CosmosClient(_options.Endpoint, _options.AuthKey, options);
        }
        else
        {
            cosmosClient = new CosmosClient(_options.Endpoint, _options.AuthKey);
        }

        Task<DatabaseResponse> response = cosmosClient.CreateDatabaseIfNotExistsAsync(_options.DatabaseName);
        response.Wait();
        return response.Result.Database;
    }

    private string GetContainerName(string containerName)
    {
        if (!string.IsNullOrEmpty(containerName))
        {
            return containerName;
        }
        else
        {
            return typeof(T).Name.Pluralize();
        }
    }
    private async Task<Container> GetContainerAsync(string containerName)
    {
        try
        {
            var containerResponse = await _cosmos.CreateContainerIfNotExistsAsync(
                GetContainerName(containerName), 
                _options.PartitionKey);

            return containerResponse.Container;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating container in Cosmos");
            throw;
        }
    }
    #endregion
}
