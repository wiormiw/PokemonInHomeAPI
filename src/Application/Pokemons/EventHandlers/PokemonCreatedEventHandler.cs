using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.EventHandlers;

public class PokemonCreatedEventHandler : INotificationHandler<PokemonCreatedEvent>
{
    private readonly ILogger<PokemonCreatedEventHandler> _logger;

    public PokemonCreatedEventHandler(ILogger<PokemonCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PokemonCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PokemonInHomeAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
