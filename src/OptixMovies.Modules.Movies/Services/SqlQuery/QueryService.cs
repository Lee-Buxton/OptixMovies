using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptixMovies.Modules.Movies.Options;
using OptixMovies.Modules.Movies.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OptixMovies.Modules.Movies.Services.SqlQuery;

public class QueryService : IQueryService
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly ILogger _logger;
    private readonly QueryOptions _options;
    private readonly Dictionary<string, string> _conditions;
    #endregion

    #region Constructor
    public QueryService(
        ILogger<QueryService> logger,
        IOptions<MoviesModuleOptions> options)
    {
        _logger = logger;
        _options = options.Value.Query;
        _conditions = new Dictionary<string, string>
        {
            {"eq", "="},
            {"lt", "<" },
            {"gt", ">" },
            {"ge", ">=" },
            {"le", "<=" },
            {"ne", "!=" },
        };
    }
    #endregion

    #region Public Methods
    public string GenerateQuery(Query query)
    {
        string sql = "SELECT * FROM c ";

        sql += GenerateWhere(query.Filter);
        sql += GenerateOrderBy(query.OrderBy);
        sql += "OFFSET " + query.Skip.ToString() + " ";
        sql += "LIMIT " + query.Top.ToString();

        return sql;
    }

    public string GenerateWhere(string filter)
    {
        if (string.IsNullOrEmpty(filter))
            return "";

        string sql = "WHERE ";

        string[] filters = filter.Split(",");

        int count = 0;

        foreach (string item in filters)
        {
            count++;

            sql += ProcessFilter(item);

            if (count < filters.Count())
                sql += " AND ";
        }

        return sql + " ";
    }

    private string ProcessFilter(string filter)
    {
        Match match = Regex.Match(filter, @"(?<field>[a-z\.]*)\s(?<condition>eq|lt|gt|ge|le|ne)\s(?<value>[a-z0-9\s\+\'\-\.\(\)|]*)", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            if (match.Groups["field"].Value == "id")
            {
                return string.Format("c.{0} {1} {2}",
                match.Groups["field"].Value,
                _conditions[match.Groups["condition"].Value.ToLower()],
                match.Groups["value"].Value
                );
            }

            return string.Format("c.listing.{0} {1} {2}",
            match.Groups["field"].Value,
            _conditions[match.Groups["condition"].Value.ToLower()],
            match.Groups["value"].Value
            );
        }
        else
        {
            throw new Exception("Syntax Error. Unable to process filter.");
        }
    }

    private string GenerateOrderBy(string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return "";

        string sql = "ORDER BY ";

        string[] sortExpressions = orderBy.Split(",");

        int count = 0;

        foreach (string item in sortExpressions)
        {
            count++;

            sql += ProcessOrderBy(item);

            if (count < sortExpressions.Count())
                sql += ", ";
        }

        return sql + " ";
    }

    private string ProcessOrderBy(string orderBy)
    {
        Match match = Regex.Match(orderBy, @"(?<field>[a-z\.]*)\s(?<direction>asc|desc)", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            return string.Format("c.listing.{0} {1}",
            match.Groups["field"].Value,
            match.Groups["direction"].Value.ToUpper()
            );
        }
        else
        {
            throw new Exception("Syntax Error. Unable to process order by.");
        }
    }

    public bool Validate(Query query)
    {
        if (string.IsNullOrEmpty(query.Filter))
            return true;

        return Validate(query.Filter);
    }

    public bool Validate(string filter)
    {
        if (string.IsNullOrEmpty(filter))
            return true;

        string[] filters = filter.Split(",");

        foreach (string item in filters)
        {
            Match match = Regex.Match(item.Trim(), @"^(?<field>[a-z\.]*)\s(?<condition>eq|lt|gt|ge|le|ne)\s(?<value>[a-z0-9\s\+\'\-\.\(\)|]*)$", RegexOptions.IgnoreCase);

            if (!match.Success)
                return false;

            if (!_options.AllowedFields.Contains(match.Groups["field"].Value))
                return false;
        }

        return true;
    }
    #endregion

    #region Override Methods

    #endregion

    #region Private Methods

    #endregion
}
