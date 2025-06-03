using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

public class GetMoveDetailQueryValidator : AbstractValidator<GetPokemonQuery>
{
    public GetMoveDetailQueryValidator()
    {
        RuleFor(x => x.PokemonId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
    }
}
