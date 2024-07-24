using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class UpdateCandidateInfoRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNo { get; set; }
    public string? Email { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
    public FileUploadRequest? Image { get; set; }
}

public class UpdateCandidateInfoRequestValidator : CustomValidator<UpdateCandidateInfoRequest>
{
    public UpdateCandidateInfoRequestValidator(IRepository<CandidateInfo> repository, IStringLocalizer<UpdateCandidateInfoRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (candidateinfo, name, ct) =>
                    await repository.FirstOrDefaultAsync(new CandidateInfoByNameSpec(name), ct)
                        is not CandidateInfo existingCandidateInfo || existingCandidateInfo.Id == candidateinfo.Id)
                .WithMessage((_, name) => T["Candidate Info {0} already Exists.", name]);
}

public class UpdateCandidateInfoRequestHandler : IRequestHandler<UpdateCandidateInfoRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CandidateInfo> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateCandidateInfoRequestHandler(IRepositoryWithEvents<CandidateInfo> repository, IStringLocalizer<UpdateCandidateInfoRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateCandidateInfoRequest request, CancellationToken cancellationToken)
    {
        var candidateinfo = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = candidateinfo
        ?? throw new NotFoundException(_t["Candidate Info {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentCandidateInfoImagePath = candidateinfo.ImagePath;
            if (!string.IsNullOrEmpty(currentCandidateInfoImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentCandidateInfoImagePath));
            }

            candidateinfo = candidateinfo.ClearImagePath();
        }

        string? candidateinfoImagePath = request.Image is not null
            ? await _file.UploadAsync<CandidateInfo>(request.Image, FileType.Image, cancellationToken)
            : null;

        candidateinfo.Update(request.Name, request.Description, request.PhoneNo, request.Email, candidateinfoImagePath);

        await _repository.UpdateAsync(candidateinfo, cancellationToken);

        return request.Id;
    }
}