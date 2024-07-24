namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class CreateCandidateInfoRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNo { get; set; }
    public string? Email { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateCandidateInfoRequestValidator : CustomValidator<CreateCandidateInfoRequest>
{
    public CreateCandidateInfoRequestValidator(IReadRepository<CandidateInfo> repository, IStringLocalizer<CreateCandidateInfoRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new CandidateInfoByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Candidate Info {0} already Exists.", name]);
}

public class CreateCandidateInfoRequestHandler : IRequestHandler<CreateCandidateInfoRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CandidateInfo> _repository;
    private readonly IFileStorageService _file;

    public CreateCandidateInfoRequestHandler(IRepositoryWithEvents<CandidateInfo> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCandidateInfoRequest request, CancellationToken cancellationToken)
    {
        string productImagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var candidateinfo = new CandidateInfo(request.Name, request.Description, request.PhoneNo, request.Email, productImagePath);

        await _repository.AddAsync(candidateinfo, cancellationToken);

        return candidateinfo.Id;
    }
}