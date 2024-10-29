using OptixMovies.Modules.Movies.Records;

namespace OptixMovies.Modules.Movies.Services.SqlQuery
{
    public interface IQueryService
    {
        string GenerateQuery(Query query);
        string GenerateWhere(string filter);
        bool Validate(Query query);
        bool ValidateFilter(string filter);
        bool ValidateOrderBy(string orderBy);
    }
}