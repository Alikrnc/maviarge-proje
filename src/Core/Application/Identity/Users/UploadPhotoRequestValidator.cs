namespace FSH.WebApi.Application.Identity.Users;

public class UploadPhotoRequestValidator : CustomValidator<UploadPhotoRequest>
{
    public UploadPhotoRequestValidator(IUserService userService, IStringLocalizer<UploadPhotoRequestValidator> T)
    {
        RuleFor(p => p.Image);
    }
}