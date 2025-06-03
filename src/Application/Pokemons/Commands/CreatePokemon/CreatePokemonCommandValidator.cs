using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.CreatePokemon;

public class CreatePokemonCommandValidator : AbstractValidator<CreatePokemonCommand>
{
    private readonly IApplicationDbContext _context;
    public CreatePokemonCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
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
            .Must(t => PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(ValidationMessage.UnsupportedTypeMessage);
        
        RuleFor(v => v.Type2)
            .Must(t => t is null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(ValidationMessage.UnsupportedTypeMessage);

        RuleFor(v => v.BaseHp)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.BaseAttack)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.BaseDefense)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.BaseSpecialAttack)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.BaseSpecialDefense)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.BaseSpeed)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
    }
    
    public async Task<bool> BeUniqueName(CreatePokemonCommand model, string name,
        CancellationToken cancellationToken)
    {
        return !await _context.PokemonSpecies
            .AnyAsync(ps => ps.Name == name, cancellationToken);
    }
}
