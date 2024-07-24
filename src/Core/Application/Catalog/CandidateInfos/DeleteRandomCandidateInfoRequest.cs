namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class DeleteRandomCandidateInfoRequest : IRequest<string>
{
}

public class DeleteRandomCandidateInfoRequestHandler : IRequestHandler<DeleteRandomCandidateInfoRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomCandidateInfoRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomCandidateInfoRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<ICandidateInfoGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}