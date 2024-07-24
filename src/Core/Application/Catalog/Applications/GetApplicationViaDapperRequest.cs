using Mapster;

namespace FSH.WebApi.Application.Catalog.Applications;

public class GetApplicationViaDapperRequest : IRequest<ApplicationDto>
{
    public Guid Id { get; set; }

    public GetApplicationViaDapperRequest(Guid id) => Id = id;
}

public class GetApplicationViaDapperRequestHandler : IRequestHandler<GetApplicationViaDapperRequest, ApplicationDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetApplicationViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetApplicationViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ApplicationDto> Handle(GetApplicationViaDapperRequest request, CancellationToken cancellationToken)
    {
        var application = await _repository.QueryFirstOrDefaultAsync<FSH.WebApi.Domain.Catalog.Application>(
            $"SELECT * FROM Catalog.\"Applications\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = application ?? throw new NotFoundException(_t["Application {0} Not Found.", request.Id]);

        // Using mapster here throws a nullreference exception because of the "JobPostingName" property
        // in ApplicationDto and the application not having a JobPosting assigned.
        return new ApplicationDto
        {
            Id = application.Id,
            JobPostingId = application.JobPostingId,
            JobPostingName = string.Empty,
            UserId = application.UserId,
            Description = application.Description,
            Name = application.Name
        };
    }
}