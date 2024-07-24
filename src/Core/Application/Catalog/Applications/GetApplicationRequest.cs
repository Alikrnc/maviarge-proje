namespace FSH.WebApi.Application.Catalog.Applications;

public class GetApplicationRequest : IRequest<ApplicationDetailsDto>
{
    public Guid Id { get; set; }

    public GetApplicationRequest(Guid id) => Id = id;
}

public class GetApplicationRequestHandler : IRequestHandler<GetApplicationRequest, ApplicationDetailsDto>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IStringLocalizer _t;

    public GetApplicationRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IStringLocalizer<GetApplicationRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ApplicationDetailsDto> Handle(GetApplicationRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<FSH.WebApi.Domain.Catalog.Application, ApplicationDetailsDto>)new ApplicationByIdWithJobPostingSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Application {0} Not Found.", request.Id]);
}