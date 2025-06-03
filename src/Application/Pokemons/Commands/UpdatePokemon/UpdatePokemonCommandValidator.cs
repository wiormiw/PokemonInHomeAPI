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
                .WithMessage(ValidationMessage.PositiveMessage);

        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage)
            .MaximumLength(255)
            .WithMessage(ValidationMessage.MaxLength255Message)
            .MustAsync(BeUniqueName)
            .WithMessage(ValidationMessage.UniqueMessage)
            .WithErrorCode("Unique");

        RuleFor(v => v.Type1)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage)
            .Must(t => t is null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(ValidationMessage.UnsupportedTypeMessage);

        RuleFor(v => v.Type2)
            .Must(t => t is null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(ValidationMessage.UnsupportedTypeMessage);

        RuleFor(v => v.BaseHp)
            .GreaterThan(0).When(v => v.BaseHp.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseHp.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);

        RuleFor(v => v.BaseAttack)
            .GreaterThan(0).When(v => v.BaseAttack.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseAttack.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);

        RuleFor(v => v.BaseDefense)
            .GreaterThan(0).When(v => v.BaseDefense.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseDefense.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);

        RuleFor(v => v.BaseSpecialAttack)
            .GreaterThan(0).When(v => v.BaseSpecialAttack.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseSpecialAttack.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);

        RuleFor(v => v.BaseSpecialDefense)
            .GreaterThan(0).When(v => v.BaseSpecialDefense.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseSpecialDefense.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);

        RuleFor(v => v.BaseSpeed)
            .GreaterThan(0).When(v => v.BaseSpeed.HasValue)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255).When(v => v.BaseSpeed.HasValue)
            .WithMessage(ValidationMessage.MaxValue255Message);
    }
    
    public async Task<bool> BeUniqueName(UpdatePokemonCommand model, string name,
        CancellationToken cancellationToken)
    {
        return !await _context.PokemonSpecies
            .Where(ps => ps.Id != model.Id)
            .AnyAsync(ps => ps.Name == name, cancellationToken);
    }
}
