namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class GetCandidateInfoRequest : IRequest<CandidateInfoDto>
{
    public Guid Id { get; set; }

    public GetCandidateInfoRequest(Guid id) => Id = id;
}

public class CandidateInfoByIdSpec : Specification<CandidateInfo, CandidateInfoDto>, ISingleResultSpecification
{
    public CandidateInfoByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetCandidateInfoRequestHandler : IRequestHandler<GetCandidateInfoRequest, CandidateInfoDto>
{
    private readonly IRepository<CandidateInfo> _repository;
    private readonly IStringLocalizer _t;

    public GetCandidateInfoRequestHandler(IRepository<CandidateInfo> repository, IStringLocalizer<GetCandidateInfoRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<CandidateInfoDto> Handle(GetCandidateInfoRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<CandidateInfo, CandidateInfoDto>)new CandidateInfoByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Candidate Info {0} Not Found.", request.Id]);
}