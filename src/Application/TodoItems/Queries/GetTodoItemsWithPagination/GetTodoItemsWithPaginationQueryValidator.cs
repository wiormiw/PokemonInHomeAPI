using PokemonInHomeAPI.Domain.Constants;

namespace PokemonInHomeAPI.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationQueryValidator : AbstractValidator<GetTodoItemsWithPaginationQuery>
{
    public GetTodoItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.ListId)
            .NotEmpty()
            .WithMessage(ValidationMessage.RequiredMessage);

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationMessage.MinValue1Message);
    }
}
