using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.DataAccess.Core
{
    public interface IPhotoRepository
    {
        Task CreateOrUpdateAsync(IFormFile photo, string id);
        Task<Stream> GetByIdAsync(string id);
    }
}