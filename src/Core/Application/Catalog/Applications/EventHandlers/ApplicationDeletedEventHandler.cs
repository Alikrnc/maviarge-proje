using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications.EventHandlers;

public class ApplicationDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<FSH.WebApi.Domain.Catalog.Application>>
{
    private readonly ILogger<ApplicationDeletedEventHandler> _logger;

    public ApplicationDeletedEventHandler(ILogger<ApplicationDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<FSH.WebApi.Domain.Catalog.Application> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}