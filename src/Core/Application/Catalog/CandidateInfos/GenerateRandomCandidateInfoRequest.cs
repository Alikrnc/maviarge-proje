namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class GenerateRandomCandidateInfoRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomCandidateInfoRequestHandler : IRequestHandler<GenerateRandomCandidateInfoRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomCandidateInfoRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomCandidateInfoRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<ICandidateInfoGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}