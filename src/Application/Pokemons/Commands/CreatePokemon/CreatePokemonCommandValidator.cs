using FluentValidation.Validators;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.CreatePokemon;

public class CreatePokemonCommandValidator : AbstractValidator<CreatePokemonCommand>
{
    public CreatePokemonCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(255)
            .WithMessage(string.Format(ErrorMessage.AttributeLengthError, "Name", 255))
            .NotEmpty()
            .WithMessage(string.Format(ErrorMessage.EmptyAttributeError, "Name"));
        
        RuleFor(v => v.Type1)
            .NotEmpty().WithMessage(string.Format(ErrorMessage.EmptyAttributeError, "Type1"))
            .Must(t => PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(string.Format(ErrorMessage.PokemonUnsupportedError, "Type1"));
        
        RuleFor(v => v.Type2)
            .Must(t => t == null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(string.Format(ErrorMessage.PokemonUnsupportedError, "Type2"));

        RuleFor(v => v.BaseHp)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseHp"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseHp", 255));
        
        RuleFor(v => v.BaseAttack)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseAttack"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseAttack", 255));
        
        RuleFor(v => v.BaseDefense)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseDefence"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseDefence", 255));;
        
        RuleFor(v => v.BaseSpecialAttack)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpecialAttack"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpecialAttack", 255));;
        
        RuleFor(v => v.BaseSpecialDefense)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpecialDefence"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpecialDefence", 255));;
        
        RuleFor(v => v.BaseSpeed)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpeed"))
            .LessThanOrEqualTo(255)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpeed", 255));
    }
}
