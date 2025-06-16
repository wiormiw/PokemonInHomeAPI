using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.AcceptTrade;

public class AcceptTradeCommandValidator : AbstractValidator<AcceptTradeCommand>
{
    public AcceptTradeCommandValidator()
    {
        RuleFor(v => v.RequestedPokemonId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
        
        RuleFor(v => v.TradeId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
    }
}
