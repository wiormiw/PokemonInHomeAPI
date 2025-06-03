using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Domain.Events;

namespace Microsoft.Extensions.DependencyInjection.Moves.EventHandlers;

public class MoveCreatedEventHandler
{
    private readonly ILogger<MoveCreatedEventHandler> _logger;

    public MoveCreatedEventHandler(ILogger<MoveCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MoveCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PokemonInHomeAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
