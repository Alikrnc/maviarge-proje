using FSH.WebApi.Application.Identity.Users;
using MediatR;
using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string Id);
}

public class FileService : IFileService
{
    private readonly ICurrentUser _currentUser;

    public FileService(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string id)
    {
        if (file == null || file.Length == 0) return null;

        // Get the file extension
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        // Determine file type
        string fileType = DetermineFileType(extension);

        // Check if the file type is supported
        if (fileType == null)
        {
            throw new InvalidOperationException("Unsupported file format.");
        }


        // Set the relative path based on the file type
        string relativePath = $"Files/{fileType}";
        string rootPath = Directory.GetCurrentDirectory();
        string fullPath = Path.Combine(rootPath, relativePath);

        // Ensure the directory exists
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }

        // Create the file name
        string fileName = $"{id}_File{extension}";
        string filePath = Path.Combine(fullPath, fileName);

        // Save the file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    private string DetermineFileType(string extension)
    {
        // Define file type categories
        var imageExtensions = new HashSet<string> { ".png", ".jpg", ".jpeg" };
        var cvExtensions = new HashSet<string> { ".pdf", ".doc", ".docx" };

        if (imageExtensions.Contains(extension))
        {
            return "Images";
        }
        else if (cvExtensions.Contains(extension))
        {
            return "CV";
        }
        else
        {
            // Default or unknown file type
            return null;
        }
    }
}