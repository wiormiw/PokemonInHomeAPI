using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

public class GetPokemonsWithPaginationQueryValidator : AbstractValidator<GetPokemonsWithPaginationQuery>
{
    public GetPokemonsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);
    }
}
