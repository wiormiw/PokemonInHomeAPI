using PokemonInHomeAPI.Domain.Constants;

namespace Microsoft.Extensions.DependencyInjection.Pokemons.Commands.CatchPokemon;

public class CatchPokemonCommandValidator : AbstractValidator<CatchPokemonCommand>
{
    public CatchPokemonCommandValidator()
    {
        RuleFor(v => v.Level)
            .NotEmpty().WithMessage(ValidationMessage.RequiredMessage)
            .GreaterThanOrEqualTo(5).WithMessage(ValidationMessage.MinPokemonLevelMessage)
            .LessThanOrEqualTo(50).WithMessage(ValidationMessage.MaxPokemonLevelMessage);
    } 
}
