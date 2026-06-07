using HRMS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace HRMS.Infrastructure.Services.FileStorage;
public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;
    public FileStorageService(IWebHostEnvironment env, IConfiguration config) { _env = env; _config = config; }
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string folder = "", CancellationToken cancellationToken = default)
    {
        var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", folder);
        Directory.CreateDirectory(uploadsPath);
        var uniqueName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(uploadsPath, uniqueName);
        using var fs = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fs, cancellationToken);
        return Path.Combine("uploads", folder, uniqueName).Replace("\\", "/");
    }
    public async Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", filePath);
        if (File.Exists(fullPath)) File.Delete(fullPath);
        await Task.CompletedTask;
    }
    public string GetPublicUrl(string filePath) => $"{_config["BaseUrl"]}/{filePath}";
}
