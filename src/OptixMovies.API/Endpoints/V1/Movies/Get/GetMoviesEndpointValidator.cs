﻿using FluentValidation;
using OptixMovies.Modules.Movies.Services.SqlQuery;

namespace OptixMovies.API.Endpoints.V1.Movies.Get;

public class GetMoviesEndpointValidator : Validator<GetMoviesEndpointRequest>
{

    #region Public Properties

    #endregion

    #region Private Properties

    #endregion

    #region Fields
    private readonly IQueryService _queryService;
    #endregion

    #region Constructor
    public GetMoviesEndpointValidator(IQueryService queryService)
    {
        _queryService = queryService;

        RuleFor(x => x.Id)
            .Must(IsIdValid)
            .WithMessage("Provided ID isn't valid");

        RuleFor(x => x.Top)
            .LessThanOrEqualTo(100)
            .WithMessage("Top can be no larger than 100")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Top should be of value of 1 or more.");

        RuleFor(x => x.Skip)
            .LessThanOrEqualTo(100)
            .WithMessage("Skip can be no larger than 100")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip should be of value of 0 or more.");

        RuleFor(x => x.Filter)
            .Must(IsFilterValid)
            .WithMessage("Unable to parse filter.");

        RuleFor(x => x.OrderBy)
            .Must(IsOrderByValid)
            .WithMessage("Unable to parse order by.");
    }
    #endregion

    #region Public Methods

    #endregion

    #region Override Methods

    #endregion

    #region Private Methods
    private bool IsIdValid(string id)
    {
        if (Guid.TryParse(id, out _))
        {
            return true;
        }
        else if (string.Empty == id)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsFilterValid(string filter)
    {
        return _queryService.ValidateFilter(filter);
    }

    private bool IsOrderByValid(string orderBy)
    {
        return _queryService.ValidateOrderBy(orderBy);
    }
    #endregion
}
