namespace FSH.WebApi.Application.Catalog.Interviews;

public class CreateInterviewRequestValidator : CustomValidator<CreateInterviewRequest>
{
    public CreateInterviewRequestValidator(IReadRepository<Interview> interviewRepo, IReadRepository<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IStringLocalizer<CreateInterviewRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await interviewRepo.FirstOrDefaultAsync(new InterviewByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Interview {0} already Exists.", name]);
        RuleFor(p => p.ApplicationId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await applicationRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Application {0} Not Found.", id]);
    }
}