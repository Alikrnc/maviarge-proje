namespace FSH.WebApi.Application.Identity.Users;

public class UploadPhotoRequest
{
    public FileUploadRequest? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
}