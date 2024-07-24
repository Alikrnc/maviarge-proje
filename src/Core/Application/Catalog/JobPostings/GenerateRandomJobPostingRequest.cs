namespace FSH.WebApi.Application.Catalog.JobPostings;

public class GenerateRandomJobPostingRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomJobPostingRequestHandler : IRequestHandler<GenerateRandomJobPostingRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomJobPostingRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomJobPostingRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IJobPostingGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}