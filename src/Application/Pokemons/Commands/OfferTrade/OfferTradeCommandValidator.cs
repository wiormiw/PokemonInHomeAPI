using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.OfferTrade;

public class OfferTradeCommandValidator : AbstractValidator<OfferTradeCommand>
{
    public OfferTradeCommandValidator()
    {
        RuleFor(v => v.Player2Id)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
        
        RuleFor(v => v.OfferedPokemonId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
        
        RuleFor(v => v.RequestedPokemonId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);
    }
}
