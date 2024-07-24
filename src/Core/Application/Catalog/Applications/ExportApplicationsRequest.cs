using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Catalog.Applications;

public class ExportApplicationsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? JobPostingId { get; set; }
    public Guid? UserId { get; set; }
}

public class ExportApplicationsRequestHandler : IRequestHandler<ExportApplicationsRequest, Stream>
{
    private readonly IReadRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportApplicationsRequestHandler(IReadRepository<FSH.WebApi.Domain.Catalog.Application> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportApplicationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportApplicationsWithJobPostingsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportApplicationsWithJobPostingsSpecification : EntitiesByBaseFilterSpec<FSH.WebApi.Domain.Catalog.Application, ApplicationExportDto>
{
    public ExportApplicationsWithJobPostingsSpecification(ExportApplicationsRequest request)
        : base(request) =>
        Query
            .Include(j => j.JobPosting)
            .Include(ci => ci.UserId)
            .Where(j => j.JobPostingId.Equals(request.JobPostingId!.Value), request.JobPostingId.HasValue);
}