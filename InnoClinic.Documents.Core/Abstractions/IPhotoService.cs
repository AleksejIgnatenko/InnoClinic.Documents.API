using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.Application.Core
{
    public interface IPhotoService
    {
        Task<string> CreatePhotoAsync(IFormFile photo);
        Task<Stream> GetPhotoByIdAsync(string id);
        Task UpdatePhotoAsync(IFormFile photo, string id);
    }
}