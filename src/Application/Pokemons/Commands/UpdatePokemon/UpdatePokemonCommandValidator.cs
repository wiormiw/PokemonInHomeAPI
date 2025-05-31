using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.UpdatePokemon;

public class UpdatePokemonCommandValidator : AbstractValidator<UpdatePokemonCommand>
{
    private readonly IApplicationDbContext _context;
    
    public UpdatePokemonCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
         RuleFor(v => v.Id)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "Id"));

        RuleFor(v => v.Name)
            .NotEmpty().When(v => v.Name != null)
            .WithMessage(string.Format(ErrorMessage.EmptyAttributeError, "Name"))
            .MaximumLength(255).When(v => v.Name != null)
            .WithMessage(string.Format(ErrorMessage.AttributeLengthError, "Name", 255))
            .MustAsync(async (command, name, cancellation) =>
                name == null || !await _context.PokemonSpecies.AnyAsync(ps => ps.Name == name && ps.Id != command.Id, cancellation))
            .WithMessage(string.Format(ErrorMessage.UniqueAttributeError, "Name"));

        RuleFor(v => v.Type1)
            .NotEmpty().When(v => v.Type1 != null)
            .WithMessage(string.Format(ErrorMessage.EmptyAttributeError, "Type1"))
            .Must(t => t == null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(string.Format(ErrorMessage.PokemonUnsupportedError, "Type1"));

        RuleFor(v => v.Type2)
            .Must(t => t == null || (!string.IsNullOrEmpty(t) && PokemonType.SupportedTypes.Any(st => st.Name == t)))
            .WithMessage(string.Format(ErrorMessage.PokemonUnsupportedError, "Type2"));

        RuleFor(v => v.BaseHp)
            .GreaterThan(0).When(v => v.BaseHp.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseHp"))
            .LessThanOrEqualTo(255).When(v => v.BaseHp.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseHp", 255));

        RuleFor(v => v.BaseAttack)
            .GreaterThan(0).When(v => v.BaseAttack.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseAttack"))
            .LessThanOrEqualTo(255).When(v => v.BaseAttack.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseAttack", 255));

        RuleFor(v => v.BaseDefense)
            .GreaterThan(0).When(v => v.BaseDefense.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseDefense"))
            .LessThanOrEqualTo(255).When(v => v.BaseDefense.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseDefense", 255));

        RuleFor(v => v.BaseSpecialAttack)
            .GreaterThan(0).When(v => v.BaseSpecialAttack.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpecialAttack"))
            .LessThanOrEqualTo(255).When(v => v.BaseSpecialAttack.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpecialAttack", 255));

        RuleFor(v => v.BaseSpecialDefense)
            .GreaterThan(0).When(v => v.BaseSpecialDefense.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpecialDefense"))
            .LessThanOrEqualTo(255).When(v => v.BaseSpecialDefense.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpecialDefense", 255));

        RuleFor(v => v.BaseSpeed)
            .GreaterThan(0).When(v => v.BaseSpeed.HasValue)
            .WithMessage(string.Format(ErrorMessage.MustPositiveAttributeError, "BaseSpeed"))
            .LessThanOrEqualTo(255).When(v => v.BaseSpeed.HasValue)
            .WithMessage(string.Format(ErrorMessage.LessThanOrEqualToAttributeError, "BaseSpeed", 255));
    }
}
