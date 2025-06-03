using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
            .WithMessage(ValidationMessage.UniqueMessage)
            .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.TodoLists
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
