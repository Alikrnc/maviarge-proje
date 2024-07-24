using Mapster;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class GetInterviewViaDapperRequest : IRequest<InterviewDto>
{
    public Guid Id { get; set; }

    public GetInterviewViaDapperRequest(Guid id) => Id = id;
}

public class GetInterviewViaDapperRequestHandler : IRequestHandler<GetInterviewViaDapperRequest, InterviewDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetInterviewViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetInterviewViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<InterviewDto> Handle(GetInterviewViaDapperRequest request, CancellationToken cancellationToken)
    {
        var interview = await _repository.QueryFirstOrDefaultAsync<Interview>(
            $"SELECT * FROM Catalog.\"Interviews\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = interview ?? throw new NotFoundException(_t["Interview {0} Not Found.", request.Id]);

        // Using mapster here throws a nullreference exception because of the "ApplicationName" property
        // in InterviewDto and the interview not having a Application assigned.
        return new InterviewDto
        {
            Id = interview.Id,
            ApplicationId = interview.ApplicationId,
            ApplicationName = string.Empty,
            Description = interview.Description
        };
    }
}