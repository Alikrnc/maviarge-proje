namespace FSH.WebApi.Application.Catalog.JobPostings;

public class DeleteRandomJobPostingRequest : IRequest<string>
{
}

public class DeleteRandomJobPostingRequestHandler : IRequestHandler<DeleteRandomJobPostingRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomJobPostingRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomJobPostingRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<IJobPostingGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}