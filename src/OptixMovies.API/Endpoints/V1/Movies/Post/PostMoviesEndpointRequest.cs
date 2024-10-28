namespace OptixMovies.API.Endpoints.V1.Movies.Post;

public class PostMoviesEndpointRequest
{
    [FromQueryParams] public Guid Id { get; set; }
}
