
namespace OptixMovies.API.Endpoints.V1.Movies.Genres.Get;

public class GetMovieGenresEndpoint : EndpointWithoutRequest<GetMovieGenresResponse>
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields

    #endregion

    #region Constructor
    public GetMovieGenresEndpoint()
    {
        
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes(
            "/Movies/Genres");
        AllowAnonymous();
        Version(1);
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return base.HandleAsync(ct);
    }
    #endregion

    #region Private Methods

    #endregion
}
