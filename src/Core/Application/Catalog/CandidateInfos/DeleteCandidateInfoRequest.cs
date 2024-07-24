using FSH.WebApi.Application.Catalog.Applications;

namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class DeleteCandidateInfoRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteCandidateInfoRequest(Guid id) => Id = id;
}

public class DeleteCandidateInfoRequestHandler : IRequestHandler<DeleteCandidateInfoRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CandidateInfo> _candidateinfoRepo;
    private readonly IReadRepository<FSH.WebApi.Domain.Catalog.Application> _applicationRepo;
    private readonly IStringLocalizer _t;

    public DeleteCandidateInfoRequestHandler(IRepositoryWithEvents<CandidateInfo> candidateinfoRepo, IReadRepository<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IStringLocalizer<DeleteCandidateInfoRequestHandler> localizer) =>
        (_candidateinfoRepo, _applicationRepo, _t) = (candidateinfoRepo, applicationRepo, localizer);

    public async Task<Guid> Handle(DeleteCandidateInfoRequest request, CancellationToken cancellationToken)
    {
        if (await _applicationRepo.AnyAsync(new ApplicationsByJobPostingSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Candidate Info cannot be deleted as it's being used."]);
        }

        var candidateinfo = await _candidateinfoRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = candidateinfo ?? throw new NotFoundException(_t["Candidate Info {0} Not Found."]);

        await _candidateinfoRepo.DeleteAsync(candidateinfo, cancellationToken);

        return request.Id;
    }
}