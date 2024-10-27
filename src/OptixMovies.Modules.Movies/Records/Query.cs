namespace OptixMovies.Modules.Movies.Records;

public record Query
{
    private int _top = 10;

    public Query()
    {

    }
    public Query(string filter, string orderBy, int top, int skip)
    {
        Filter = filter;
        OrderBy = orderBy;
        Top = top;
        Skip = skip;
    }

    public string Filter { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
    public int Top
    {
        get => _top;
        set
        {
            if (value! > 0)
                _top = (value <= 50) ? value : 50;
        }
    }
    public int Skip { get; set; }
}
