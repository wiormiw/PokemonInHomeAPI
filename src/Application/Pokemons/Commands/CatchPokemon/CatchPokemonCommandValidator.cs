using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.CatchPokemon;

public class CatchPokemonCommandValidator : AbstractValidator<CatchPokemonCommand>
{
    public CatchPokemonCommandValidator()
    {
        RuleFor(v => v.Level)
            .NotEmpty().WithMessage(ValidationMessage.RequiredMessage)
            .GreaterThanOrEqualTo(5).WithMessage(ValidationMessage.MinPokemonLevelMessage)
            .LessThanOrEqualTo(50).WithMessage(ValidationMessage.MaxPokemonLevelMessage);

        RuleFor(v => v.Nickname)
            .NotEmpty().WithMessage(ValidationMessage.RequiredMessage);
    } 
}
