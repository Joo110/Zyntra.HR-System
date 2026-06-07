namespace HRMS.Application.Common.Interfaces;
public interface IFileStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string folder = "", CancellationToken cancellationToken = default);
    Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);
    string GetPublicUrl(string filePath);
}
