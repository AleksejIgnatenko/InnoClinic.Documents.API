using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.Application.Core
{
    public interface IValidationService
    {
        Dictionary<string, string> Validation(IFormFile photo);
    }
}