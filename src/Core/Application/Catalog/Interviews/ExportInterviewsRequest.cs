using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class ExportInterviewsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? ApplicationId { get; set; }
}

public class ExportInterviewsRequestHandler : IRequestHandler<ExportInterviewsRequest, Stream>
{
    private readonly IReadRepository<Interview> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportInterviewsRequestHandler(IReadRepository<Interview> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportInterviewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportInterviewsWithApplicationsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportInterviewsWithApplicationsSpecification : EntitiesByBaseFilterSpec<Interview, InterviewExportDto>
{
    public ExportInterviewsWithApplicationsSpecification(ExportInterviewsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Application)
            .Where(p => p.ApplicationId.Equals(request.ApplicationId!.Value), request.ApplicationId.HasValue);
}