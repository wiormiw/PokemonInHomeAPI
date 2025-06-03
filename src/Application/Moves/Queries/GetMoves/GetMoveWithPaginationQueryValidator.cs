using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

public class GetMovesWithPaginationQueryValidator : AbstractValidator<GetMovesWithPaginationQuery>
{
    public GetMovesWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);
    }
}
