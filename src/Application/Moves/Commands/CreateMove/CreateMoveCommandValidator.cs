using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Moves.Commands.CreateMove;

public class CreateMoveCommandValidator : AbstractValidator<CreateMoveCommand>
{
    private readonly IApplicationDbContext _context;
    
    public CreateMoveCommandValidator(IApplicationDbContext context)
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

        RuleFor(v => v.Type)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage)
            .Must(t => t is null || PokemonType.SupportedTypes.Any(st => st.Name == t))
            .WithMessage(ValidationMessage.UnsupportedTypeMessage);
        
        RuleFor(v => v.Power)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.Accuracy)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
        
        RuleFor(v => v.Pp)
            .GreaterThan(0)
            .WithMessage(ValidationMessage.PositiveMessage)
            .LessThanOrEqualTo(255)
            .WithMessage(ValidationMessage.MaxValue255Message);
    }
    
    public async Task<bool> BeUniqueName(CreateMoveCommand model, string name,
        CancellationToken cancellationToken)
    {
        return !await _context.Moves
            .AnyAsync(mv => mv.Name == name, cancellationToken);
    }
}
