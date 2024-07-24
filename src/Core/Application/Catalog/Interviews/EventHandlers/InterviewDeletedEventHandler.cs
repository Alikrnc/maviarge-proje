using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews.EventHandlers;

public class InterviewDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Interview>>
{
    private readonly ILogger<InterviewDeletedEventHandler> _logger;

    public InterviewDeletedEventHandler(ILogger<InterviewDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Interview> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}