using FSH.WebApi.Application.Catalog.Interviews;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class InterviewsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Interviews)]
    [OpenApiOperation("Search interviews using available filters.", "")]
    public Task<PaginationResponse<InterviewDto>> SearchAsync(SearchInterviewsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Interviews)]
    [OpenApiOperation("Get interview details.", "")]
    public Task<InterviewDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetInterviewRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Interviews)]
    [OpenApiOperation("Get interview details via dapper.", "")]
    public Task<InterviewDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetInterviewViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Interviews)]
    [OpenApiOperation("Create a new interview.", "")]
    public Task<Guid> CreateAsync(CreateInterviewRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Interviews)]
    [OpenApiOperation("Update a interview.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateInterviewRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Interviews)]
    [OpenApiOperation("Delete a interview.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteInterviewRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Interviews)]
    [OpenApiOperation("Export a interviews.", "")]
    public async Task<FileResult> ExportAsync(ExportInterviewsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "InterviewExports");
    }
    }