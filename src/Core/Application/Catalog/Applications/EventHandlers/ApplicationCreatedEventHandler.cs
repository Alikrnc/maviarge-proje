using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications.EventHandlers;

public class ApplicationCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<FSH.WebApi.Domain.Catalog.Application>>
{
    private readonly ILogger<ApplicationCreatedEventHandler> _logger;

    public ApplicationCreatedEventHandler(ILogger<ApplicationCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<FSH.WebApi.Domain.Catalog.Application> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}