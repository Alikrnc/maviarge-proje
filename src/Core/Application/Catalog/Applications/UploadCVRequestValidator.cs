namespace FSH.WebApi.Application.Catalog.Applications;

public class UploadCVRequestValidator : CustomValidator<UploadCVRequest>
{
    public UploadCVRequestValidator()
    {
        RuleFor(p => p.CV);
        RuleFor(p => p.CVPath);
    }
}