using FSH.WebApi.Application.Catalog.Applications;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class ApplicationsController : VersionedApiController
{
    private readonly IFileService _fileService;

    public ApplicationsController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("search_applications")]
    [MustHavePermission(FSHAction.Search, FSHResource.Applications)]
    [OpenApiOperation("Search applications using available filters.", "")]
    public Task<PaginationResponse<ApplicationDto>> SearchAsync(SearchApplicationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("search_applicants")]
    [MustHavePermission(FSHAction.Search, FSHResource.Applications)]
    [OpenApiOperation("Search applicants using available filters.", "")]
    public Task<PaginationResponse<ApplicantDto>> SearchAsync(SearchApplicantsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Applications)]
    [OpenApiOperation("Get application details.", "")]
    public Task<ApplicationDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetApplicationRequest(id));
    }

    [HttpPost("upload-cv")]
    [OpenApiOperation("Upload a CV file.", "")]
    public async Task<IActionResult> UploadCV([FromForm] UploadCVRequest request)
    {
        if (request.CV == null || request.CV.Length == 0)
            return BadRequest("No file uploaded.");

        string hostpath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "CV");
        string fileName = await _fileService.UploadFileAsync(request.CV, request.Id);

        if (fileName == null)
            return BadRequest("File upload failed.");

        // Update the CVPath in the request
        request.CVPath = Path.Combine(hostpath, fileName);

        // Send the updated request using Mediator
        return Ok(await Mediator.Send(request));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Applications)]
    [OpenApiOperation("Get application details via dapper.", "")]
    public Task<ApplicationDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetApplicationViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Applications)]
    [OpenApiOperation("Create a new application.", "")]
    public Task<Guid> CreateAsync(CreateApplicationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Applications)]
    [OpenApiOperation("Update a application.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateApplicationRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Applications)]
    [OpenApiOperation("Delete a application.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteApplicationRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Applications)]
    [OpenApiOperation("Export a applications.", "")]
    public async Task<FileResult> ExportAsync(ExportApplicationsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ApplicationExports");
    }
    }