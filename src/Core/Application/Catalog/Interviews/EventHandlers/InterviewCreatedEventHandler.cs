using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews.EventHandlers;

public class InterviewCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Interview>>
{
    private readonly ILogger<InterviewCreatedEventHandler> _logger;

    public InterviewCreatedEventHandler(ILogger<InterviewCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Interview> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}