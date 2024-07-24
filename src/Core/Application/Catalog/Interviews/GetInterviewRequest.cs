namespace FSH.WebApi.Application.Catalog.Interviews;

public class GetInterviewRequest : IRequest<InterviewDetailsDto>
{
    public Guid Id { get; set; }

    public GetInterviewRequest(Guid id) => Id = id;
}

public class GetInterviewRequestHandler : IRequestHandler<GetInterviewRequest, InterviewDetailsDto>
{
    private readonly IRepository<Interview> _repository;
    private readonly IStringLocalizer _t;

    public GetInterviewRequestHandler(IRepository<Interview> repository, IStringLocalizer<GetInterviewRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<InterviewDetailsDto> Handle(GetInterviewRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Interview, InterviewDetailsDto>)new InterviewByIdWithApplicationSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Interview {0} Not Found.", request.Id]);
}