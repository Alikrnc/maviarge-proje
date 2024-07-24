using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications.EventHandlers;

public class ApplicationUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<FSH.WebApi.Domain.Catalog.Application>>
{
    private readonly ILogger<ApplicationUpdatedEventHandler> _logger;

    public ApplicationUpdatedEventHandler(ILogger<ApplicationUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<FSH.WebApi.Domain.Catalog.Application> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}