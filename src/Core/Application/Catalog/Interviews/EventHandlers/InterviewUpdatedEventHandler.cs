using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews.EventHandlers;

public class InterviewUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Interview>>
{
    private readonly ILogger<InterviewUpdatedEventHandler> _logger;

    public InterviewUpdatedEventHandler(ILogger<InterviewUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Interview> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}