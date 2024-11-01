using FastEndpoints;
using FluentValidation;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace OptixMovies.API.Endpoints.Movies.Get;

public class GetMoviesEndpointValidator : Validator<GetMoviesEndpointRequest>
{
    #region Fields
    private const string _validTitleRegex = @"^[a-z0-9\s\+\-\.\(\)\:]*$";
    private const string _validGenreRegex = @"^[a-z0-9\s]*$";
    #endregion

    #region Constructor
    public GetMoviesEndpointValidator()
    {
        RuleFor(x => x)
            .Must(IsOrderByValid)
            .WithMessage("OrderByReleaseDate and OrderByTitle, only one can be set to true.")
            .Must(IsTitleValid)
            .WithMessage("Title only alphnumeric characters, plus the following special characters:" +
            "+ (Plus Sign), " +
            "- (Hyphen), " +
            ". (Period), " +
            ": (Colon), " +
            "() (Parenthesis), " +
            "(Space)")
            .Must(IsGenreValid)
            .WithMessage("Genres only support alphabetic characters and the space character.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize can be no larger than 100")
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize should be of value of 1 or more.");

        RuleFor(x => x.PageNumber)
            .LessThanOrEqualTo(10000)
            .WithMessage("PageNumber can be no larger than 10000")
            .GreaterThanOrEqualTo(0)
            .WithMessage("PageNumber should be of value of 0 or more.");
    }
    #endregion

    #region Private Methods
    private bool IsOrderByValid(GetMoviesEndpointRequest request)
    {
        return request.OrderByReleaseDate.Value ^ request.OrderByTitle.Value;
    }

    private bool IsTitleValid(GetMoviesEndpointRequest request)
    {
        if (!string.IsNullOrEmpty(request.Title))
            return Regex.IsMatch(request.Title, _validTitleRegex, RegexOptions.IgnoreCase);
        else
            return true;
    }

    private bool IsGenreValid(GetMoviesEndpointRequest request)
    {
        if(request.Genres.Length != 0)
        {
            foreach(var genre in request.Genres)
            {
                if(!Regex.IsMatch(genre, _validGenreRegex, RegexOptions.IgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return true;
        }
    }
    #endregion
}