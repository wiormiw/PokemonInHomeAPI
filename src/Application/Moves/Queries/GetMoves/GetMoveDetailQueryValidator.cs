using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

public class GetMoveDetailQueryValidator : AbstractValidator<GetMoveQuery>
{
    public GetMoveDetailQueryValidator()
    {
        RuleFor(x => x.MoveId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
    }
}
