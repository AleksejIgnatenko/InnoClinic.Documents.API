using InnoClinic.Documents.Application.Core;
using InnoClinic.Documents.Core.Exceptions;
using InnoClinic.Documents.DataAccess.Core;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IValidationService _validationService;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IValidationService validationService, IPhotoRepository photoRepository)
        {
            _validationService = validationService;
            _photoRepository = photoRepository;
        }

        public async Task<string> CreatePhotoAsync(IFormFile photo)
        {
            var validationErrors = _validationService.Validation(photo);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            var id = Guid.NewGuid();

            await _photoRepository.CreateOrUpdateAsync(photo, id.ToString());

            return id.ToString();
        }

        public async Task<Stream> GetPhotoByIdAsync(string id)
        {
            return await _photoRepository.GetByIdAsync(id.ToString());
        }

        public async Task UpdatePhotoAsync(IFormFile photo, string id)
        {
            var validationErrors = _validationService.Validation(photo);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _photoRepository.CreateOrUpdateAsync(photo, id.ToString());
        }
    }
}
